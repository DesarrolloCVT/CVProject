using CommunityToolkit.Mvvm.ComponentModel;
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

        private static int IdBancoCeldaSeleccionada;
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

        #region Encapsulado

        public ObservableCollection<BancoModel> BancoInfoCollection
        {
            get { return Bancos; }
            set { Bancos = value; }
        }

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

        #endregion

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

        #region Metodos 
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
            try
            {
                if (e.RowData is BancoModel banco)
                {
                    IdBancoCeldaSeleccionada = banco.Id_Banco;
                    CodigoCeldaSeleccionada = banco.Codigo;
                    NombreBancoCeldaSeleccionada = banco.Nombre;
                    // Aquí puedes manejar la lógica de negocio sin tocar la vista
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error CeldaTocada BancoViewModel: " + ex.Message);
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
                    EliminarBanco(IdBancoCeldaSeleccionada);
                    CargarBancos();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
                Console.WriteLine("Error Eliminar BancoViewModel: " + Ex.Message);
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
                Console.WriteLine("Error Agregar BancoViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void InsertarBanco()
        {
            try
            {
                if (CodigoBancoIngresado != 0 && !string.IsNullOrEmpty(NombreBancoIngresado))
                {
                    AgregarBanco(new BancoModel(0, CodigoBancoIngresado, NombreBancoIngresado));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error InsertarBanco BancoViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        private async void Editar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    try
                    {
                        OldBank = new BancoModel(IdBancoCeldaSeleccionada, CodigoCeldaSeleccionada, NombreBancoCeldaSeleccionada)
                        {
                            Id_Banco = IdBancoCeldaSeleccionada,
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
            catch (Exception Ex)
            {
                Console.WriteLine("Error Editar BancoViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        private async void Detalles()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    await _navigationService.NavigateToAsync<NavigationViewModel>("Banco_Detalle", IdBancoCeldaSeleccionada);
                    //Application.Current.MainPage.DisplayAlert("Alerta", "Has seleccionado una Fila Valida", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Debe seleccionar una fila valida", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("ERROR: Detalles ViewModelBancoDetalle: " + Ex.Message);
            }
        }

        private async Task CargarBancos()
        {
            try
            {
                var bancos = await _bancoService.GetBancosAsync();
                Bancos.Clear();
                foreach (var banco in bancos)
                {
                    Bancos.Add(banco);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Editar BancoViewModel: " + Ex.Message);
            }
        }

        private async Task AgregarBanco(BancoModel banco)
        {
            try
            {
                if (await _bancoService.AddBancoAsync(banco))
                {
                    Bancos.Add(banco);
                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente. ", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error, ya existe una entrada asignada con este Codigo.", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error AgregarBanco BancoViewModel: " + Ex.Message);
            }
        }

        private async Task EliminarBanco(int id)
        {
            try
            {
                if (await _bancoService.DeleteBancoAsync(id))
                {
                    var banco = Bancos.FirstOrDefault(p => p.Id_Banco == id);
                    if (banco != null)
                    {
                        Bancos.Remove(banco);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error EliminarBanco BancoViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public void Update()
        {
            try
            {
                ActualizarBanco((BancoModel)OldBank);
                _navigationService.GoBackAsync();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Update BancoViewModel: " + Ex.Message);
            }
        }

        private async Task ActualizarBanco(BancoModel AntiguoBanco)
        {
            try
            {
                Console.WriteLine("EditCodigoBanco: " + EditCodigoBanco);
                Console.WriteLine("EditNombreBanco: " + EditNombreBanco);

                var id = IdBancoCeldaSeleccionada;
                var cod = EditCodigoBanco;
                var name = EditNombreBanco;

                NewBank = new BancoModel(id, cod, name)
                {
                    Id_Banco = id,
                    Codigo = cod,
                    Nombre = name
                };

                if (await _bancoService.UpdateBancoAsync((BancoModel)NewBank))
                {
                    //Remove Old Product
                    Bancos.Remove(AntiguoBanco);

                    //Add new product
                    Bancos.Add((BancoModel)NewBank);
                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");

                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error al editar", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error ActualizarBanco BancoViewModel: " + Ex.Message);
            }
        }
        #endregion
    }
}

