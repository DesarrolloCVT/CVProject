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

        private const int MAX_INT = 2147483647; // Máximo permitido en SQL Server para INT

        public static int _idBanco;

        private static object _oldbankDetail;
        private object NewbankDetail;

        private static int IdBancoDetalleCeldaSeleccionada;
        private int CodigoBancoCeldaSeleccionada;
        private int NumeroBancoDetalleCeldaSeleccionada;
        private int SaldoBancoDetalleCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly BancoDetalleService _bancoDetalleService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<BancoDetalleModel> bancoDetalles;

        private string _filterText;

        private int _codigoBancoDetallesIngresadoText;
        private long _numerobancoDetallesIngresadoText;
        private long _saldobancoDetallesIngresadoText;

        private int _editCodigoBancoDetalles;
        private long _editNumerobancoDetalles;
        private long _editSaldobancoDetalles;
        

        public ICommand CargarBancoDetallesCommand { get; }
        public ICommand AgregarBancoDetalleCommand { get; }
        public ICommand EliminarBancoDetalleCommand { get; }
        public ICommand ActualizarBancoDetalleCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Encapsulado
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

        public long NumerobancoDetallesIngresado
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

        public long EditNumeroBancoDetalles
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

        public long SaldobancoDetallesIngresado
        {
            get => _saldobancoDetallesIngresadoText;
            set
            {
                if (_saldobancoDetallesIngresadoText != value)
                {
                    _saldobancoDetallesIngresadoText = value;
                    OnPropertyChanged(nameof(SaldobancoDetallesIngresado));
                }
            }
        }

        public long EditSaldobancoDetalles
        {
            get => _editSaldobancoDetalles;
            set
            {
                if (_editSaldobancoDetalles != value)
                {
                    _editSaldobancoDetalles = value;
                    OnPropertyChanged(nameof(EditSaldobancoDetalles));
                }
            }
        }

        public int Id_Banco
        {
            get => _idBanco;
            set
            {
                if (_idBanco != value)
                {
                    _idBanco = value;
                    OnPropertyChanged(nameof(Id_Banco));
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

        #region Metodos 

        [RelayCommand]
        public async void Volver()
        {
            try
            {
                bancoDetalles.Clear();
                Id_Banco = 0;
                await _navigationService.GoBackAsync();
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error. ", "Ok");
                Console.WriteLine("Error Volver IngresoDetalleViewsModel: " + Ex.Message);
            }
        }

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
                    IdBancoDetalleCeldaSeleccionada = bancoDetalleModel.Id_Banco_Detalle;
                    CodigoBancoCeldaSeleccionada = bancoDetalleModel.Codigo_Banco;
                    NumeroBancoDetalleCeldaSeleccionada = bancoDetalleModel.Numero;
                    SaldoBancoDetalleCeldaSeleccionada = bancoDetalleModel.Saldo;
                }
            }
            catch (Exception Ex)
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
                    EliminarBancoDatlles(IdBancoDetalleCeldaSeleccionada);
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
                    AgregarBancoDetalle(new BancoDetalleModel(IdBancoDetalleCeldaSeleccionada, Id_Banco, CodigoBancoDetalleIngresado, (int)NumerobancoDetallesIngresado, (int)SaldobancoDetallesIngresado));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion. ", "Ok");
                }
            }
            catch (Exception Ex)
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
                    OldbankDetail = new BancoDetalleModel(IdBancoDetalleCeldaSeleccionada, Id_Banco, CodigoBancoCeldaSeleccionada, NumeroBancoDetalleCeldaSeleccionada, SaldoBancoDetalleCeldaSeleccionada)
                    {
                        Id_Banco = Id_Banco,
                        Codigo_Banco = CodigoBancoCeldaSeleccionada,
                        Numero = NumeroBancoDetalleCeldaSeleccionada,
                        Saldo = SaldoBancoDetalleCeldaSeleccionada
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
                var bancoDetalle = await _bancoDetalleService.GetBancoDetalleFilterByIdAsync(Id_Banco);
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
                if (!(NumerobancoDetallesIngresado > MAX_INT))
                {
                    if (await _bancoDetalleService.AddBancoDetalleAsync(bancoDetalleModel))
                    {
                        bancoDetalles.Add(bancoDetalleModel);
                        Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente. ", "Ok");
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error, ya existe una entrada asignada con este Codigo.", "Ok");
                    }
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", $"El valor no puede superar {MAX_INT}", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error AgregarBancoDetalle BancoDetalleViewModel: " + Ex.Message);
            }
        }

        private async Task EliminarBancoDatlles(int id)
        {
            try
            {
                if (await _bancoDetalleService.DeleteBancoDetalleAsync(id))
                {
                    var bancoDetalle = bancoDetalles.FirstOrDefault(p => p.Id_Banco_Detalle == id);
                    if (bancoDetalle != null)
                    {
                        bancoDetalles.Remove(bancoDetalle);
                    }
                }
            }
            catch (Exception Ex)
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

                var id = IdBancoDetalleCeldaSeleccionada;
                var id_banco = Id_Banco;
                var codigo = EditCodigoBancoDetalles;
                var numero = (int)EditNumeroBancoDetalles;
                var saldo = (int)EditSaldobancoDetalles;

                NewbankDetail = new BancoDetalleModel(id, id_banco, codigo, numero, saldo)
                {
                    Codigo_Banco = codigo,
                    Numero = numero,
                    Saldo = saldo
                    
                };

                if (!(EditNumeroBancoDetalles > MAX_INT))
                {
                    if (await _bancoDetalleService.UpdateBancoDetalleAsync((BancoDetalleModel)NewbankDetail))
                    {
                        //Remove Old Product
                        bancoDetalles.Remove(AntiguoBancoDetalle);
                        //Add new product
                        bancoDetalles.Add((BancoDetalleModel)NewbankDetail);
                        Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente. ", "Ok");
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error al actualizar. ", "Ok");
                    }
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", $"El valor no puede superar {MAX_INT}", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error ActualizarBancoDetalle BancoDetalleViewModel: " + Ex.Message);
            }
        }
        #endregion
    }
}
