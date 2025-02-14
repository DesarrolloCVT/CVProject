﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using Syncfusion.Maui.DataGrid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopAplicationCV.ViewModel
{
    public partial class BancoViewModel : BaseViewModel
    {
        #region Variables

        private static object _oldBank;
        private object NewBank;

        private int CodigoCeldaSeleccionada;
        private string NombreBancoCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly BancoService _bancoService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<BancoModel> Bancos;

        private string _filterText; 
        private int _codigoBancoIngresadoText;
        private string _nombreBancoIngresadoText;

        private int _editCodigoBanco;
        private string _editNombreBanco;

        public ICommand CargarBancosCommand { get; }
        public ICommand AgregarBancoCommand { get; }
        public ICommand EliminarBancoCommand { get; }
        public ICommand ActualizarBancoCommand { get; }
        public ICommand CeldaTocadaCommand { get; }
        #endregion

        #region Inicializadores
        public ObservableCollection<BancoModel> BancoInfoCollection
        {
            get { return Bancos; }
            set { Bancos = value; }
        }
        #endregion

        // Propiedad para enlazar el texto del filtro desde la vista
        public string FilterText
        {
            get => _filterText;
            set
            {
                if (_filterText != value)
                {
                    _filterText = value;
                    OnPropertyChanged(nameof(FilterText));
                    // Llamar a la acción para actualizar el filtro
                    ApplyFilterAction?.Invoke();
                }
            }
        }

        public object OldBank
        {
            get => _oldBank;
            set
            {
                if (_oldBank != value)
                {
                    _oldBank = value;
                }
            }
        }

        public string NombreBancoIngresado
        {
            get => _nombreBancoIngresadoText;
            set
            {
                if (_nombreBancoIngresadoText != value)
                {
                    _nombreBancoIngresadoText = value;
                    OnPropertyChanged(nameof(NombreBancoIngresado));
                }
            }
        }

        public string EditNombreBanco
        {
            get => _editNombreBanco;
            set
            {
                if (_editNombreBanco != value)
                {
                    _editNombreBanco = value;
                    OnPropertyChanged(nameof(EditNombreBanco));
                }
            }
        }

        public int CodigoBancoIngresado
        {
            get => _codigoBancoIngresadoText;
            set
            {
                if (_codigoBancoIngresadoText != value)
                {
                    _codigoBancoIngresadoText = value;
                    OnPropertyChanged(nameof(CodigoBancoIngresado));
                }
            }
        }

        public int EditCodigoBanco
        {
            get => _editCodigoBanco;
            set
            {
                if (_editCodigoBanco != value)
                {
                    _editCodigoBanco = value;
                    OnPropertyChanged(nameof(EditCodigoBanco));
                }
            }
        }

        // Acción para establecer la lógica del filtro
        public Action ApplyFilterAction { get; set; }

        #region Constructores

        public BancoViewModel(INavigationService navigationService)
        {
            _bancoService = new BancoService();
            Bancos = new ObservableCollection<BancoModel>();

            _navigationService = navigationService;
            CargarBancos();

            CeldaTocadaCommand = new Command<DataGridCellTappedEventArgs>(CeldaTocada);
        }

        #endregion

        [RelayCommand]
        public void Cancelar()
        {
            _navigationService.GoBackAsync();
        }

        // Lógica de filtrado como delegado
        public Predicate<object> GetFilter()
        {
            return item =>
            {
                if (item is BancoModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Nombre.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Codigo.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        // Método que se ejecuta cuando se toca una celda
        private void CeldaTocada(DataGridCellTappedEventArgs e)
        {
            if (e.RowData is BancoModel banco)
            {
                CodigoCeldaSeleccionada = banco.Codigo;
                NombreBancoCeldaSeleccionada = banco.Nombre;
                // Aquí puedes manejar la lógica de negocio sin tocar la vista
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [RelayCommand]
        public void GridCargado()
        {
            CargarBancos();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    EliminarBanco(CodigoCeldaSeleccionada);
                    CargarBancos();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
            }
        }

        [RelayCommand]
        public async void Agregar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Banco");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
            }
        }

        [RelayCommand]
        public async void InsertarBanco()
        {
            if (CodigoBancoIngresado != 0 && !string.IsNullOrEmpty(NombreBancoIngresado))
            {
                AgregarBanco(new BancoModel(CodigoBancoIngresado, NombreBancoIngresado));
                Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente", "Ok");
                _navigationService.GoBackAsync();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
            }
        }

        [RelayCommand]
        private async void Editar()
        {
            if (selectedIndex >= 0)
            {
                try
                {
                    OldBank = new BancoModel(CodigoCeldaSeleccionada, NombreBancoCeldaSeleccionada)
                    {
                        Codigo = CodigoCeldaSeleccionada,
                        Nombre = NombreBancoCeldaSeleccionada
                    };

                    await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Banco", OldBank);
                }
                catch (Exception Ex)
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                }
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Debe seleccionar una fila valida", "Ok");
            }
        }

        private async Task CargarBancos()
        {
            var bancos = await _bancoService.GetBancosAsync();
            Bancos.Clear();
            foreach (var banco in bancos)
            {
                Bancos.Add(banco);
            }
        }

        private async Task AgregarBanco(BancoModel banco)
        {
            if (await _bancoService.AddBancoAsync(banco))
            {
                Bancos.Add(banco);
            }
        }

        private async Task EliminarBanco(int codigo)
        {
            if (await _bancoService.DeleteBancoAsync(codigo))
            {
                var banco = Bancos.FirstOrDefault(p => p.Codigo == codigo);
                if (banco != null)
                {
                    Bancos.Remove(banco);
                }
            }
        }

        [RelayCommand]
        public void Update()
        {
            ActualizarBanco((BancoModel)OldBank);
            Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");
            _navigationService.GoBackAsync();
        }

        private async Task ActualizarBanco(BancoModel AntiguoBanco)
        {
            Console.WriteLine("EditCodigoBanco: " + EditCodigoBanco);
            Console.WriteLine("EditNombreBanco: " + EditNombreBanco);

            var cod = EditCodigoBanco;
            var name = EditNombreBanco;

            NewBank = new BancoModel(cod, name)
            {
                Codigo = cod,
                Nombre = name
            };

            if (await _bancoService.UpdateBancoAsync((BancoModel)NewBank))
            {
                //Remove Old Product
                Bancos.Remove(AntiguoBanco);

                //Add new product
                Bancos.Add((BancoModel)NewBank);

            }
        }
    }
}

