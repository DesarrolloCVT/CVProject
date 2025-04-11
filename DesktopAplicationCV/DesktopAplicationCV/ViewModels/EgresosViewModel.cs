using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.Views;
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
using Windows.Services.Maps;

namespace DesktopAplicationCV.ViewModels
{
    public partial class EgresosViewModel : BaseViewModel
    {
        #region Variables

        [ObservableProperty]
        private List<MonedaModel> _monedas;

        [ObservableProperty]
        private List<SubtiposModel> _subtipos;

        [ObservableProperty]
        private List<MetodoPagoModel> _metodopagos;

        [ObservableProperty]
        private List<BancoModel> _bancos;

        private static object _oldEgreso;
        private object NewEgreso;

        private static int IdEgresoCeldaSeleccionada;
        private int FolioEgresoCeldaSeleccionada;
        private string TipoEgresoCeldaSeleccionada;
        private string SubtipoEgresoCeldaSeleccionada;
        private string MonedaEgresoCeldaSeleccionada;
        private DateTime FechaEgresoCeldaSeleccionada;
        private string ClienteEgresoCeldaSeleccionada;
        private string MetodoPagoEgresoCeldaSeleccionada;
        private string BancoEgresoCeldaSeleccionada;
        private string CuentaEgresoCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly EgresosService _egresoService;
        private readonly ApiService _apiService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<EgresosModel> Egresos;

        private string _filterText;
        private string _tituloPagina;

        private int _folioEgresosIngresadoText;
        private string _tipoEgresosIngresadoText;
        private string _subtipoEgresosIngresadoText;
        private string _monedaEgresosIngresadoText;
        private DateTime _fechaEgresosIngresadoText;
        private string _clienteEgresosIngresadoText;
        private string _metodoPagoEgresosIngresadoText;
        private string _bancoEgresosIngresadoText;
        private string _cuentaEgresosIngresadoText;


        private int _editIdEgresos;
        private int _editFolioEgresos;
        private string _editTipoEgresos;
        private string _editSubTipoEgresos;
        private string _editMonedaEgresos;
        private DateTime _editFechaEgresos;
        private string _editClienteEgresos;
        private string _editMetodoPagoEgresos;
        private string _editBancoEgresos;
        private string _editCuentaEgresos;

        public ICommand CargarEgresosCommand { get; }
        public ICommand AgregarEgresoCommand { get; }
        public ICommand EliminarEgresoCommand { get; }
        public ICommand ActualizarEgresoCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Encapsulado

        public ObservableCollection<EgresosModel> EgresosInfoCollection
        {
            get { return Egresos; }
            set { Egresos = value; }
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

        public string TituloPagina
        {
            get => _tituloPagina;
            set
            {
                _tituloPagina = value;
                OnPropertyChanged(nameof(TituloPagina));
            }
        }

        public object OldEgreso
        {
            get => _oldEgreso;
            set
            {
                if (_oldEgreso != value)
                {
                    _oldEgreso = value;
                }
            }
        }

        public int FolioEgresosIngresadoText
        {
            get => _folioEgresosIngresadoText;
            set
            {
                if (_folioEgresosIngresadoText != value)
                {
                    _folioEgresosIngresadoText = value;
                    OnPropertyChanged(nameof(FolioEgresosIngresadoText));
                }
            }
        }

        public int EditFolioEgresos
        {
            get => _editFolioEgresos;
            set
            {
                if (_editFolioEgresos != value)
                {
                    _editFolioEgresos = value;
                    OnPropertyChanged(nameof(EditFolioEgresos));
                }
            }
        }

        public int EditIdEgresos
        {
            get => _editIdEgresos;
            set
            {
                if (_editIdEgresos != value)
                {
                    _editIdEgresos = value;
                    OnPropertyChanged(nameof(EditIdEgresos));
                }
            }
        }

        public string TipoEgresosIngresadoText
        {
            get => _tipoEgresosIngresadoText;
            set
            {
                if (_tipoEgresosIngresadoText != value)
                {
                    _tipoEgresosIngresadoText = value;
                    OnPropertyChanged(nameof(TipoEgresosIngresadoText));
                }
            }
        }

        public string EditTipoEgresos
        {
            get => _editTipoEgresos;
            set
            {
                if (_editTipoEgresos != value)
                {
                    _editTipoEgresos = value;
                    OnPropertyChanged(nameof(EditTipoEgresos));
                }
            }
        }

        public string SubtipoEgresosIngresadoText
        {
            get => _subtipoEgresosIngresadoText;
            set
            {
                if (_subtipoEgresosIngresadoText != value)
                {
                    _subtipoEgresosIngresadoText = value;
                    OnPropertyChanged(nameof(SubtipoEgresosIngresadoText));
                }
            }
        }

        public string EditSubTipoEgresos
        {
            get => _editSubTipoEgresos;
            set
            {
                if (_editSubTipoEgresos != value)
                {
                    _editSubTipoEgresos = value;
                    OnPropertyChanged(nameof(EditSubTipoEgresos));
                }
            }
        }

        public string MonedaEgresosIngresadoText
        {
            get => _monedaEgresosIngresadoText;
            set
            {
                if (_monedaEgresosIngresadoText != value)
                {
                    _monedaEgresosIngresadoText = value;
                    OnPropertyChanged(nameof(MonedaEgresosIngresadoText));
                }
            }
        }

        public string EditMonedaEgresos
        {
            get => _editMonedaEgresos;
            set
            {
                if (_editMonedaEgresos != value)
                {
                    _editMonedaEgresos = value;
                    OnPropertyChanged(nameof(EditMonedaEgresos));
                }
            }
        }

        public DateTime FechaEgresosIngresadoText
        {
            get => _fechaEgresosIngresadoText;
            set
            {
                if (_fechaEgresosIngresadoText != value)
                {
                    _fechaEgresosIngresadoText = value;
                    OnPropertyChanged(nameof(FechaEgresosIngresadoText));
                }
            }
        }

        public DateTime EditFechaEgresos
        {
            get => _editFechaEgresos;
            set
            {
                if (_editFechaEgresos != value)
                {
                    _editFechaEgresos = value;
                    OnPropertyChanged(nameof(EditFechaEgresos));
                }
            }
        }

        public string ClienteEgresosIngresadoText
        {
            get => _clienteEgresosIngresadoText;
            set
            {
                if (_clienteEgresosIngresadoText != value)
                {
                    _clienteEgresosIngresadoText = value;
                    OnPropertyChanged(nameof(ClienteEgresosIngresadoText));
                }
            }
        }

        public string EditClienteEgresos
        {
            get => _editClienteEgresos;
            set
            {
                if (_editClienteEgresos != value)
                {
                    _editClienteEgresos = value;
                    OnPropertyChanged(nameof(EditClienteEgresos));
                }
            }
        }

        public string MetodoPagoEgresosIngresadoText
        {
            get => _metodoPagoEgresosIngresadoText;
            set
            {
                if (_metodoPagoEgresosIngresadoText != value)
                {
                    _metodoPagoEgresosIngresadoText = value;
                    OnPropertyChanged(nameof(MetodoPagoEgresosIngresadoText));
                }
            }
        }

        public string EditMetodoPagoEgresos
        {
            get => _editMetodoPagoEgresos;
            set
            {
                if (_editMetodoPagoEgresos != value)
                {
                    _editMetodoPagoEgresos = value;
                    OnPropertyChanged(nameof(EditMetodoPagoEgresos));
                }
            }
        }

        public string BancoEgresosIngresadoText
        {
            get => _bancoEgresosIngresadoText;
            set
            {
                if (_bancoEgresosIngresadoText != value)
                {
                    _bancoEgresosIngresadoText = value;
                    OnPropertyChanged(nameof(BancoEgresosIngresadoText));
                }
            }
        }

        public string EditBancoEgresos
        {
            get => _editBancoEgresos;
            set
            {
                if (_editBancoEgresos != value)
                {
                    _editBancoEgresos = value;
                    OnPropertyChanged(nameof(EditBancoEgresos));
                }
            }
        }

        public string CuentaEgresosIngresadoText
        {
            get => _cuentaEgresosIngresadoText;
            set
            {
                if (_cuentaEgresosIngresadoText != value)
                {
                    _cuentaEgresosIngresadoText = value;
                    OnPropertyChanged(nameof(CuentaEgresosIngresadoText));
                }
            }
        }

        public string EditCuentaEgresos
        {
            get => _editCuentaEgresos;
            set
            {
                if (_editCuentaEgresos != value)
                {
                    _editCuentaEgresos = value;
                    OnPropertyChanged(nameof(EditCuentaEgresos));
                }
            }
        }

        #endregion

        #region Constructores

        public EgresosViewModel(INavigationService navigationService, ApiService apiService)
        {
            _apiService = apiService;
            _egresoService = new EgresosService();
            Egresos = new ObservableCollection<EgresosModel>();

            _navigationService = navigationService;
            CargarEgresos();
            CargarComboBoxes();

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
                if (item is EgresosModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Folio.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Tipo_Transaccion.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Subtipo_Transaccion.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Moneda.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Fecha.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Cliente.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Metodo_Pago.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Banco.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Cuenta.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        // Método que se ejecuta cuando se toca una celda
        private void CeldaTocada(DataGridCellTappedEventArgs e)
        {
            try
            {
                if (e.RowData is EgresosModel egresos)
                {
                    IdEgresoCeldaSeleccionada = egresos.Id_Egreso;
                    FolioEgresoCeldaSeleccionada = egresos.Folio;
                    TipoEgresoCeldaSeleccionada = egresos.Tipo_Transaccion;
                    SubtipoEgresoCeldaSeleccionada = egresos.Subtipo_Transaccion;
                    MonedaEgresoCeldaSeleccionada = egresos.Moneda;
                    FechaEgresoCeldaSeleccionada = egresos.Fecha;
                    ClienteEgresoCeldaSeleccionada = egresos.Cliente;
                    MetodoPagoEgresoCeldaSeleccionada = egresos.Metodo_Pago;
                    BancoEgresoCeldaSeleccionada = egresos.Banco;
                    CuentaEgresoCeldaSeleccionada = egresos.Cuenta;
                    // Aquí puedes manejar la lógica de negocio sin tocar la vista
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error CeldaTocada IngresosViewModel: " + ex.Message);
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
            CargarEgresos();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    EliminarEgresos(FolioEgresoCeldaSeleccionada);
                    CargarEgresos();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
                Console.WriteLine("Error Eliminar IngresosViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void Agregar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Egresos");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                Console.WriteLine("Error Agregar EgresosViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void InsertarEgresos()
        {
            try
            {
                if (FolioEgresosIngresadoText != 0 && !string.IsNullOrEmpty(TipoEgresosIngresadoText) && !string.IsNullOrEmpty(MonedaEgresosIngresadoText)
                && !string.IsNullOrEmpty(ClienteEgresosIngresadoText) && !string.IsNullOrEmpty(MetodoPagoEgresosIngresadoText)
                && !string.IsNullOrEmpty(BancoEgresosIngresadoText) && !string.IsNullOrEmpty(CuentaEgresosIngresadoText))
                {
                    AgregarEgresos(new EgresosModel(IdEgresoCeldaSeleccionada, FolioEgresosIngresadoText, TipoEgresosIngresadoText, SubtipoEgresosIngresadoText, MonedaEgresosIngresadoText,
                        FechaEgresosIngresadoText, ClienteEgresosIngresadoText, MetodoPagoEgresosIngresadoText, BancoEgresosIngresadoText,
                        CuentaEgresosIngresadoText));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error InsertarEgresos EgresosViewModel: " + Ex.Message);
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
                        OldEgreso = new EgresosModel(4, FolioEgresoCeldaSeleccionada, TipoEgresoCeldaSeleccionada, SubtipoEgresoCeldaSeleccionada, MonedaEgresoCeldaSeleccionada,
                            FechaEgresoCeldaSeleccionada, ClienteEgresoCeldaSeleccionada, MetodoPagoEgresoCeldaSeleccionada, BancoEgresoCeldaSeleccionada,
                            CuentaEgresoCeldaSeleccionada)
                        {
                            Id_Egreso = IdEgresoCeldaSeleccionada,
                            Folio = FolioEgresoCeldaSeleccionada,
                            Tipo_Transaccion = TipoEgresoCeldaSeleccionada,
                            Subtipo_Transaccion = SubtipoEgresoCeldaSeleccionada,
                            Moneda = MonedaEgresoCeldaSeleccionada,
                            Fecha = FechaEgresoCeldaSeleccionada,
                            Cliente = ClienteEgresoCeldaSeleccionada,
                            Metodo_Pago = MetodoPagoEgresoCeldaSeleccionada,
                            Banco = BancoEgresoCeldaSeleccionada,
                            Cuenta = CuentaEgresoCeldaSeleccionada
                        };

                        await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Egresos", OldEgreso);
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
                Console.WriteLine("Error Editar EgresosViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        private void Detalles()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Has seleccionado una Fila Valida", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Debe seleccionar una fila valida", "Ok");
                }
            }
            catch (Exception Ex)
            {

            }
        }

        private async Task CargarEgresos()
        {
            try
            {
                var egresos = await _egresoService.GetEgresosAsync();
                Egresos.Clear();
                foreach (var egreso in egresos)
                {
                    Egresos.Add(egreso);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error CargarEgresos EgresosViewModel: " + Ex.Message);
            }
        }

        private async Task CargarComboBoxes()
        {
            try
            {
                /*Bancos = await _apiService.GetBancosAsync();
                Monedas = await _apiService.GetMonedasAsync();
                Metodopagos = await _apiService.GetMetodoPagoAsync();
                Subtipos = await _egresoService.GetSubtiposFilterByIdAsync("egreso");*/
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error CargarComboBoxes IngresosViewModel: " + Ex.Message);
            }
        }

        private async Task AgregarEgresos(EgresosModel egreso)
        {
            try
            {
                if (await _egresoService.AddEgresoAsync(egreso))
                {
                    Egresos.Add(egreso);
                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error, ya existe una entrada asignada con este Folio.", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error AgregarEgresos EgresosViewModel: " + Ex.Message);
            }
        }

        private async Task EliminarEgresos(int folio)
        {
            try
            {
                if (await _egresoService.DeleteEgresoAsync(folio))
                {
                    var egreso = Egresos.FirstOrDefault(p => p.Folio == folio);
                    if (egreso != null)
                    {
                        Egresos.Remove(egreso);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error EliminarEgresos EgresosViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public void Update()
        {
            try
            {
                ActualizarEgresos((EgresosModel)OldEgreso);
                Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");
                _navigationService.GoBackAsync();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Update EgresosViewModel: " + Ex.Message);
            }

        }

        private async Task ActualizarEgresos(EgresosModel AntiguoEgreso)
        {
            try
            {
                Console.WriteLine("EditFolioEgresos: " + EditFolioEgresos);
                Console.WriteLine("EditTipoEgresos: " + EditTipoEgresos);
                Console.WriteLine("EditSubTipoEgresos: " + EditSubTipoEgresos);
                Console.WriteLine("EditMonedaEgresos: " + EditMonedaEgresos);
                Console.WriteLine("EditFechaEgresos: " + EditFechaEgresos);
                Console.WriteLine("EditClienteEgresos: " + EditClienteEgresos);
                Console.WriteLine("EditMetodoPagoEgresos: " + EditMetodoPagoEgresos);
                Console.WriteLine("EditBancoEgresos: " + EditBancoEgresos);
                Console.WriteLine("EditCuentaEgresos: " + EditCuentaEgresos);

                var id = IdEgresoCeldaSeleccionada;
                var folio = EditFolioEgresos;
                var tipo = EditTipoEgresos;
                var subtipo = EditSubTipoEgresos;
                var moneda = EditMonedaEgresos;
                DateTime fecha = EditFechaEgresos;
                var cliente = EditClienteEgresos;
                var metodoPago = EditMetodoPagoEgresos;
                var banco = EditBancoEgresos;
                var cuenta = EditCuentaEgresos;

                /*var date = fecha.ToString("yyyy-MM-dd",CultureInfo.InvariantCulture);
                var NewDate = Convert.ToDateTime(date);*/


                NewEgreso = new EgresosModel(id, folio, tipo, subtipo, moneda, fecha, cliente, metodoPago, banco, cuenta)
                {
                    Id_Egreso = id,
                    Folio = folio,
                    Tipo_Transaccion = tipo,
                    Subtipo_Transaccion = subtipo,
                    Moneda = moneda,
                    Fecha = fecha,
                    Cliente = cliente,
                    Metodo_Pago = metodoPago,
                    Banco = banco,
                    Cuenta = cuenta
                };

                if (await _egresoService.UpdateEgresoAsync((EgresosModel)NewEgreso))
                {
                    //Remove Old Product
                    Egresos.Remove(AntiguoEgreso);

                    //Add new product
                    Egresos.Add((EgresosModel)NewEgreso);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error ActualizarEgresos EgresosViewModel: " + Ex.Message);
            }
        }
        #endregion
    }
}
