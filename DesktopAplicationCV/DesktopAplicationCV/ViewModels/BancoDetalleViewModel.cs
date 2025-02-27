using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using Syncfusion.Maui.DataGrid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DesktopAplicationCV.ViewModel
{
    public partial class BancoDetalleViewModel : BaseViewModel
    {
        #region Variables

        private static object _oldbankDetail;
        private object NewbankDetail;

        private int CodigoBancoCeldaSeleccionada;
        private int NumeroBancoDetalleCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly BancoDetalleService _bancoDetalleService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<BancoDetalleModel> bancoDetalles;

        private string _filterText;

        private int _codigoBancoDetallesIngresadoText;
        private int _numerobancoDetallesIngresadoText;

        private int _editCodigoBancoDetalles;
        private int _editNumerobancoDetalles;
        

        public ICommand CargarBancoDetallesCommand { get; }
        public ICommand AgregarBancoDetalleCommand { get; }
        public ICommand EliminarBancoDetalleCommand { get; }
        public ICommand ActualizarBancoDetalleCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Inicializadores
        public ObservableCollection<BancoDetalleModel> BancoDetalleInfoCollection
        {
            get { return bancoDetalles; }
            set { bancoDetalles = value; }
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
        // Acción para establecer la lógica del filtro
        public Action ApplyFilterAction { get; set; }

        public object OldbankDetail
        {
            get => _oldbankDetail;
            set
            {
                if (_oldbankDetail != value)
                {
                    _oldbankDetail = value;
                }
            }
        }

        public int CodigoBancoDetalleIngresado
        {
            get => _codigoBancoDetallesIngresadoText;
            set
            {
                if (_codigoBancoDetallesIngresadoText != value)
                {
                    _codigoBancoDetallesIngresadoText = value;
                    OnPropertyChanged(nameof(CodigoBancoDetalleIngresado));
                }
            }
        }

        public int EditCodigoBancoDetalles
        {
            get => _editCodigoBancoDetalles;
            set
            {
                if (_editCodigoBancoDetalles != value)
                {
                    _editCodigoBancoDetalles = value;
                    OnPropertyChanged(nameof(EditCodigoBancoDetalles));
                }
            }
        }

        public int NumerobancoDetallesIngresado
        {
            get => _numerobancoDetallesIngresadoText;
            set
            {
                if (_numerobancoDetallesIngresadoText != value)
                {
                    _numerobancoDetallesIngresadoText = value;
                    OnPropertyChanged(nameof(NumerobancoDetallesIngresado));
                }
            }
        }

        public int EditNumeroBancoDetalles
        {
            get => _editNumerobancoDetalles;
            set
            {
                if (_editNumerobancoDetalles != value)
                {
                    _editNumerobancoDetalles = value;
                    OnPropertyChanged(nameof(EditNumeroBancoDetalles));
                }
            }
        }

        #endregion

        #region Constructores
        public BancoDetalleViewModel(INavigationService navigationService)
        {
            _bancoDetalleService = new BancoDetalleService();
            bancoDetalles = new ObservableCollection<BancoDetalleModel>();

            _navigationService = navigationService;
            CargarBancoDetalles();

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
                if (item is BancoDetalleModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Codigo_Banco.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Numero.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        // Método que se ejecuta cuando se toca una celda
        private void CeldaTocada(DataGridCellTappedEventArgs e)
        {
            try
            {
                if (e.RowData is BancoDetalleModel bancoDetalleModel)
                {
                    CodigoBancoCeldaSeleccionada = bancoDetalleModel.Codigo_Banco;
                    NumeroBancoDetalleCeldaSeleccionada = bancoDetalleModel.Numero;
                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine("Error CeldaTocada BancoDetalleViewModel: " + Ex.Message);
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
            CargarBancoDetalles();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    EliminarBancoDatlles(CodigoBancoCeldaSeleccionada);
                    CargarBancoDetalles();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso de eliminar item", "Ok");
                Console.WriteLine("Error Eliminar BancoDetalleViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void Agregar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Banco_Detalle");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso de agregar item. ", "Ok");
                Console.WriteLine("Error Agregar BancoDetalleViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void InsertarBancoDetalle()
        {
            try
            {
                if (CodigoBancoDetalleIngresado != 0 && NumerobancoDetallesIngresado != 0)
                {
                    AgregarBancoDetalle(new BancoDetalleModel(CodigoBancoDetalleIngresado, NumerobancoDetallesIngresado));
                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente. ", "Ok");
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion. ", "Ok");
                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine("Error InsertarBancoDetalle BancoDetalleViewModel: " + Ex.Message);
            }
            
        }

        [RelayCommand]
        private async void Editar()
        {
            if (selectedIndex >= 0)
            {
                try
                {
                    OldbankDetail = new BancoDetalleModel(CodigoBancoCeldaSeleccionada, NumeroBancoDetalleCeldaSeleccionada)
                    {
                        Codigo_Banco = CodigoBancoCeldaSeleccionada,
                        Numero = NumeroBancoDetalleCeldaSeleccionada
                    };

                    await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Banco_Detalle", OldbankDetail);
                }
                catch (Exception Ex)
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso de edicion. ", "Ok");
                    Console.WriteLine("Error Editar BancoDetalleViewModel: " + Ex.Message);
                }
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Debe seleccionar una fila valida. ", "Ok");
            }
        }

        private async Task CargarBancoDetalles()
        {
            try
            {
                var bancoDetalle = await _bancoDetalleService.GetBancoDetallesAsync();
                bancoDetalles.Clear();
                foreach (var bancoDetail in bancoDetalle)
                {
                    bancoDetalles.Add(bancoDetail);
                }
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error CargarBancoDetalles BancoDetalleViewModel: " + Ex.Message);
            }
        }

        private async Task AgregarBancoDetalle(BancoDetalleModel bancoDetalleModel)
        {
            try
            {
                if (await _bancoDetalleService.AddBancoDetalleAsync(bancoDetalleModel))
                {
                    bancoDetalles.Add(bancoDetalleModel);
                }
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error AgregarBancoDetalle BancoDetalleViewModel: " + Ex.Message);
            }
        }

        private async Task EliminarBancoDatlles(int codigo)
        {
            try
            {
                if (await _bancoDetalleService.DeleteBancoDetalleAsync(codigo))
                {
                    var bancoDetalle = bancoDetalles.FirstOrDefault(p => p.Codigo_Banco == codigo);
                    if (bancoDetalle != null)
                    {
                        bancoDetalles.Remove(bancoDetalle);
                    }
                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine("Error EliminarBancoDatlles BancoDetalleViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public void Update()
        {
            try
            {
                ActualizarBancoDetalle((BancoDetalleModel)OldbankDetail);
                Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");
                _navigationService.GoBackAsync();
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error Update BancoDetalleViewModel: " + Ex.Message);
            }
        }

        private async Task ActualizarBancoDetalle(BancoDetalleModel AntiguoBancoDetalle)
        {
            try
            {
                Console.WriteLine("EditCodigoBancoDetalles: " + EditCodigoBancoDetalles);
                Console.WriteLine("EditNumeroBancoDetalles: " + EditNumeroBancoDetalles);

                var codigo = EditCodigoBancoDetalles;
                var numero = EditNumeroBancoDetalles;

                NewbankDetail = new BancoDetalleModel(codigo, numero)
                {
                    Codigo_Banco = codigo,
                    Numero = numero
                };

                if (await _bancoDetalleService.UpdateBancoDetalleAsync((BancoDetalleModel)NewbankDetail))
                {
                    //Remove Old Product
                    bancoDetalles.Remove(AntiguoBancoDetalle);

                    //Add new product
                    bancoDetalles.Add((BancoDetalleModel)NewbankDetail);

                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine("Error ActualizarBancoDetalle BancoDetalleViewModel: " + Ex.Message);
            }
        }
    }
}
