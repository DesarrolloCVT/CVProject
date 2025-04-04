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

namespace DesktopAplicationCV.ViewModels
{
    public partial class TransaccionesViewModel : BaseViewModel
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

        [ObservableProperty]
        private List<TipoModel> _tipos;

        [ObservableProperty]
        private List<SocioNegocioModel> _cliente;

        [ObservableProperty]
        private List<CuentasModel> _cuentas;

        private static object _oldTransaccion;
        private object NewTransaccion;

        private static int IdTransaccionCeldaSeleccionada;
        private int FolioTransaccionCeldaSeleccionada;
        private string TipoTransaccionCeldaSeleccionada;
        private string SubTipoTransaccionCeldaSeleccionada;
        private string MonedaTransaccionCeldaSeleccionada;
        private DateTime FechaTransaccionCeldaSeleccionada;
        private string ClienteTransaccionCeldaSeleccionada;
        private string MetodoPagoTransaccionCeldaSeleccionada;
        private string BancoTransaccionCeldaSeleccionada;
        private string CuentaTransaccionCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly AuxService _auxService;
        private readonly TransaccionService _transaccionService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<TransaccionesModel> Transacciones;

        private string _filterText;
        private string _tituloPagina;


        private TipoModel _tipoSeleccionado;
        private SubtiposModel _subtipoSeleccionado;
        private MonedaModel _monedaSeleccionado;
        private MetodoPagoModel _metodoDePagoSeleccionado;
        private BancoModel _bancoSeleccionado;
        private SocioNegocioModel _clienteSeleccionado;
        private CuentasModel _cuentasSeleccionado;



        private int _folioTransaccionIngresadoText;
        private string _tipoTransaccionIngresadoText;
        private string _subTipoTransaccionIngresadoText;
        private string _monedaTransaccionIngresadoText;
        private DateTime _fechaTransaccionIngresadoText;
        private string _clienteTransaccionIngresadoText;
        private string _metodoPagoTransaccionIngresadoText;
        private string _bancoTransaccionIngresadoText;
        private string _cuentaTransaccionIngresadoText;


        private int _editFolioTransaccion;
        private string _editTipoTransaccion;
        private string _editSubTipoTransaccion;
        private string _editMonedaTransaccion;
        private DateTime _editFechaTransaccion;
        private string _editClienteTransaccion;
        private string _editMetodoPagoTransaccion;
        private string _editBancoTransaccion;
        private string _editCuentaTransaccion;

        public ICommand CargarTransaccionCommand { get; }
        public ICommand AgregarTransaccionCommand { get; }
        public ICommand EliminarTransaccionCommand { get; }
        public ICommand ActualizarTransaccionCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Encapsulado

        public ObservableCollection<TransaccionesModel> TransaccionInfoCollection
        {
            get { return Transacciones; }
            set { Transacciones = value; }
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

        public object OldTransaccion
        {
            get => _oldTransaccion;
            set
            {
                if (_oldTransaccion != value)
                {
                    _oldTransaccion = value;
                }
            }
        }      

        public int FolioTransaccionIngresadoText
        {
            get => _folioTransaccionIngresadoText;
            set
            {
                if (_folioTransaccionIngresadoText != value)
                {
                    _folioTransaccionIngresadoText = value;
                    OnPropertyChanged(nameof(FolioTransaccionIngresadoText));
                }
            }
        }

        public int EditFolioTransaccion
        {
            get => _editFolioTransaccion;
            set
            {
                if (_editFolioTransaccion != value)
                {
                    _editFolioTransaccion = value;
                    OnPropertyChanged(nameof(EditFolioTransaccion));
                }
            }
        }

        public string TipoTransaccionIngresadoText
        {
            get => _tipoTransaccionIngresadoText;
            set
            {
                if (_tipoTransaccionIngresadoText != value)
                {
                    _tipoTransaccionIngresadoText = value;
                    OnPropertyChanged(nameof(TipoTransaccionIngresadoText));
                }
            }
        }

        public TipoModel TipoSeleccionado
        {
            get => _tipoSeleccionado;
            set
            {
                if (_tipoSeleccionado != value)
                {
                    _tipoSeleccionado = value;
                    TipoTransaccionIngresadoText = value.Tipo_Dato.Trim();
                    OnPropertyChanged(nameof(TipoSeleccionado));
                    OnPropertyChanged(nameof(TipoTransaccionIngresadoText)); // Para actualizar la vista
                }
            }
        }

        public string EditTipoTransaccion
        {
            get => _editTipoTransaccion;
            set
            {
                if (_editTipoTransaccion != value)
                {
                    _editTipoTransaccion = value;
                    OnPropertyChanged(nameof(EditTipoTransaccion));
                }
            }
        }

        public string SubTipoTransaccionIngresadoText
        {
            get => _subTipoTransaccionIngresadoText;
            set
            {
                if (_subTipoTransaccionIngresadoText != value)
                {
                    _subTipoTransaccionIngresadoText = value;
                    OnPropertyChanged(nameof(SubTipoTransaccionIngresadoText));
                }
            }
        }

        public SubtiposModel SubTipoSeleccionado
        {
            get => _subtipoSeleccionado;
            set
            {
                if (_subtipoSeleccionado != value)
                {
                    _subtipoSeleccionado = value;
                    SubTipoTransaccionIngresadoText = value.Nombre.Trim();
                    OnPropertyChanged(nameof(SubTipoSeleccionado));
                    OnPropertyChanged(nameof(SubTipoTransaccionIngresadoText)); // Para actualizar la vista
                }
            }
        }

        public string EditSubTipoTransaccion
        {
            get => _editSubTipoTransaccion;
            set
            {
                if (_editSubTipoTransaccion != value)
                {
                    _editSubTipoTransaccion = value;
                    OnPropertyChanged(nameof(EditSubTipoTransaccion));
                }
            }
        }

        public string MonedaTransaccionIngresadoText
        {
            get => _monedaTransaccionIngresadoText;
            set
            {
                if (_monedaTransaccionIngresadoText != value)
                {
                    _monedaTransaccionIngresadoText = value;
                    OnPropertyChanged(nameof(MonedaTransaccionIngresadoText));
                }
            }
        }

        public MonedaModel MonedaSeleccionado
        {
            get => _monedaSeleccionado;
            set
            {
                if (_monedaSeleccionado != value)
                {
                    _monedaSeleccionado = value;
                    MonedaTransaccionIngresadoText = value.Nombre.Trim();
                    OnPropertyChanged(nameof(MonedaSeleccionado));
                    OnPropertyChanged(nameof(MonedaTransaccionIngresadoText)); // Para actualizar la vista
                }
            }
        }

        public string EditMonedaTransaccion
        {
            get => _editMonedaTransaccion;
            set
            {
                if (_editMonedaTransaccion != value)
                {
                    _editMonedaTransaccion = value;
                    OnPropertyChanged(nameof(EditMonedaTransaccion));
                }
            }
        }

        public DateTime FechaTransaccionIngresadoText
        {
            get => _fechaTransaccionIngresadoText;
            set
            {
                if (_fechaTransaccionIngresadoText != value)
                {
                    _fechaTransaccionIngresadoText = value;
                    OnPropertyChanged(nameof(FechaTransaccionIngresadoText));
                }
            }
        }

        public DateTime EditFechaTransaccion
        {
            get => _editFechaTransaccion;
            set
            {
                if (_editFechaTransaccion != value)
                {
                    _editFechaTransaccion = value;
                    OnPropertyChanged(nameof(EditFechaTransaccion));
                }
            }
        }

        public string ClienteTransaccionIngresadoText
        {
            get => _clienteTransaccionIngresadoText;
            set
            {
                if (_clienteTransaccionIngresadoText != value)
                {
                    _clienteTransaccionIngresadoText = value;
                    OnPropertyChanged(nameof(ClienteTransaccionIngresadoText));
                }
            }
        }

        public SocioNegocioModel ClienteSeleccionado
        {
            get => _clienteSeleccionado;
            set
            {
                if (_clienteSeleccionado != value)
                {
                    _clienteSeleccionado = value;
                    ClienteTransaccionIngresadoText = value.Nombre.Trim();
                    OnPropertyChanged(nameof(ClienteSeleccionado));
                    OnPropertyChanged(nameof(ClienteTransaccionIngresadoText)); // Para actualizar la vista
                }
            }
        }

        public string EditClienteTransaccion
        {
            get => _editClienteTransaccion;
            set
            {
                if (_editClienteTransaccion != value)
                {
                    _editClienteTransaccion = value;
                    OnPropertyChanged(nameof(EditClienteTransaccion));
                }
            }
        }

        public string MetodoPagoTransaccionIngresadoText
        {
            get => _metodoPagoTransaccionIngresadoText;
            set
            {
                if (_metodoPagoTransaccionIngresadoText != value)
                {
                    _metodoPagoTransaccionIngresadoText = value;
                    OnPropertyChanged(nameof(MetodoPagoTransaccionIngresadoText));
                }
            }
        }

        public MetodoPagoModel MetodoPagoSeleccionado
        {
            get => _metodoDePagoSeleccionado;
            set
            {
                if (_metodoDePagoSeleccionado != value)
                {
                    _metodoDePagoSeleccionado = value;
                    MetodoPagoTransaccionIngresadoText = value.Nombre.Trim();
                    OnPropertyChanged(nameof(MetodoPagoSeleccionado));
                    OnPropertyChanged(nameof(MetodoPagoTransaccionIngresadoText)); // Para actualizar la vista
                }
            }
        }

        public string EditMetodoPagoTransaccion
        {
            get => _editMetodoPagoTransaccion;
            set
            {
                if (_editMetodoPagoTransaccion != value)
                {
                    _editMetodoPagoTransaccion = value;
                    OnPropertyChanged(nameof(EditMetodoPagoTransaccion));
                }
            }
        }

        public string BancoTransaccionIngresadoText
        {
            get => _bancoTransaccionIngresadoText;
            set
            {
                if (_bancoTransaccionIngresadoText != value)
                {
                    _bancoTransaccionIngresadoText = value;
                    OnPropertyChanged(nameof(BancoTransaccionIngresadoText));
                }
            }
        }

        public BancoModel BancoSeleccionado
        {
            get => _bancoSeleccionado;
            set
            {
                if (_bancoSeleccionado != value)
                {
                    _bancoSeleccionado = value;
                    BancoTransaccionIngresadoText = value.Nombre.Trim();
                    OnPropertyChanged(nameof(BancoSeleccionado));
                    OnPropertyChanged(nameof(BancoTransaccionIngresadoText)); // Para actualizar la vista
                }
            }
        }

        public string EditBancoTransaccion
        {
            get => _editBancoTransaccion;
            set
            {
                if (_editBancoTransaccion != value)
                {
                    _editBancoTransaccion = value;
                    OnPropertyChanged(nameof(EditBancoTransaccion));
                }
            }
        }

        public string CuentaTransaccionIngresadoText
        {
            get => _cuentaTransaccionIngresadoText;
            set
            {
                if (_cuentaTransaccionIngresadoText != value)
                {
                    _cuentaTransaccionIngresadoText = value;
                    OnPropertyChanged(nameof(CuentaTransaccionIngresadoText));
                }
            }
        }

        public CuentasModel CuentasSeleccionado
        {
            get => _cuentasSeleccionado;
            set
            {
                if (_cuentasSeleccionado != value)
                {
                    _cuentasSeleccionado = value;
                    CuentaTransaccionIngresadoText = value.Nombre.Trim();
                    OnPropertyChanged(nameof(CuentasSeleccionado));
                    OnPropertyChanged(nameof(CuentaTransaccionIngresadoText)); // Para actualizar la vista
                }
            }
        }

        public string EditCuentaTransaccion
        {
            get => _editCuentaTransaccion;
            set
            {
                if (_editCuentaTransaccion != value)
                {
                    _editCuentaTransaccion = value;
                    OnPropertyChanged(nameof(EditCuentaTransaccion));
                }
            }
        }

        #endregion

        #region Constructores

        public TransaccionesViewModel(INavigationService navigationService, AuxService auxService)
        {
            _auxService = auxService;
            _transaccionService = new TransaccionService();
            Transacciones = new ObservableCollection<TransaccionesModel>();

            _navigationService = navigationService;
            CargarGrillaIngresos();
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
                if (item is TransaccionesModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Id_Transaccion.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
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
                if (e.RowData is TransaccionesModel transacciones)
                {
                    IdTransaccionCeldaSeleccionada = transacciones.Id_Transaccion;
                    FolioTransaccionCeldaSeleccionada = transacciones.Folio;
                    TipoTransaccionCeldaSeleccionada = transacciones.Tipo_Transaccion;
                    SubTipoTransaccionCeldaSeleccionada = transacciones.Subtipo_Transaccion;
                    MonedaTransaccionCeldaSeleccionada = transacciones.Moneda;
                    FechaTransaccionCeldaSeleccionada = transacciones.Fecha;
                    ClienteTransaccionCeldaSeleccionada = transacciones.Cliente;
                    MetodoPagoTransaccionCeldaSeleccionada = transacciones.Metodo_Pago;
                    BancoTransaccionCeldaSeleccionada = transacciones.Banco;
                    CuentaTransaccionCeldaSeleccionada = transacciones.Cuenta;
                    // Aquí puedes manejar la lógica de negocio sin tocar la vista
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error CeldaTocada TransaccionesViewModel: " + ex.Message);
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
            CargarGrillaIngresos();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    EliminarTransacciones(FolioTransaccionCeldaSeleccionada);
                    CargarGrillaIngresos();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
                Console.WriteLine("Error Eliminar TransaccionesViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void Agregar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Transacciones");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                Console.WriteLine("Error Agregar TransaccionesViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void InsertarTransacciones()
        {
            try
            {
                if (FolioTransaccionIngresadoText != 0 && !string.IsNullOrEmpty(TipoTransaccionIngresadoText) && !string.IsNullOrEmpty(MonedaTransaccionIngresadoText)
                && !string.IsNullOrEmpty(ClienteTransaccionIngresadoText) && !string.IsNullOrEmpty(MetodoPagoTransaccionIngresadoText)
                && !string.IsNullOrEmpty(BancoTransaccionIngresadoText) && !string.IsNullOrEmpty(CuentaTransaccionIngresadoText))
                {
                    AgregarTransacciones(new TransaccionesModel(IdTransaccionCeldaSeleccionada, FolioTransaccionIngresadoText, TipoTransaccionIngresadoText, SubTipoTransaccionIngresadoText, MonedaTransaccionIngresadoText,
                        FechaTransaccionIngresadoText, ClienteTransaccionIngresadoText, MetodoPagoTransaccionIngresadoText, BancoTransaccionIngresadoText,
                        CuentaTransaccionIngresadoText));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error InsertarTransaccionesDetalle IngresosViewModel: " + Ex.Message);
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
                        OldTransaccion = new TransaccionesModel(IdTransaccionCeldaSeleccionada, FolioTransaccionCeldaSeleccionada, TipoTransaccionCeldaSeleccionada, SubTipoTransaccionCeldaSeleccionada, MonedaTransaccionCeldaSeleccionada,
                            FechaTransaccionCeldaSeleccionada, ClienteTransaccionCeldaSeleccionada, MetodoPagoTransaccionCeldaSeleccionada, BancoTransaccionCeldaSeleccionada,
                            CuentaTransaccionCeldaSeleccionada)
                        {
                            Id_Transaccion = IdTransaccionCeldaSeleccionada,
                            Folio = FolioTransaccionCeldaSeleccionada,
                            Tipo_Transaccion = TipoTransaccionCeldaSeleccionada,
                            Subtipo_Transaccion = SubTipoTransaccionCeldaSeleccionada,
                            Moneda = MonedaTransaccionCeldaSeleccionada,
                            Fecha = FechaTransaccionCeldaSeleccionada,
                            Cliente = ClienteTransaccionCeldaSeleccionada,
                            Metodo_Pago = MetodoPagoTransaccionCeldaSeleccionada,
                            Banco = BancoTransaccionCeldaSeleccionada,
                            Cuenta = CuentaTransaccionCeldaSeleccionada
                        };

                        await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Transacciones", OldTransaccion);
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
                Console.WriteLine("Error Editar TransaccionesViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        private async void Detalles()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    await _navigationService.NavigateToAsync<NavigationViewModel>("Transacciones_Detalle", IdTransaccionCeldaSeleccionada);
                    //await _navigationService.NavigateToAsync<NavigationViewModel>("Transacciones_Detalle");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Debe seleccionar una fila valida", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Catch Detalles: " + Ex.Message);
            }
        }

        private async Task CargarGrillaIngresos()
        {
            try
            {
                var transacciones = await _transaccionService.GetTransaccionesAsync();
                Transacciones.Clear();
                foreach (var transaccion in transacciones)
                {
                    Transacciones.Add(transaccion);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error CargarGrillaIngresos IngresosViewModel: " + Ex.Message);
            }
        }

        private async Task CargarComboBoxes()
        {
            try
            {
                Tipos = await _auxService.GetTiposAsync();
                Bancos = await _auxService.GetBancosAsync();
                Monedas = await _auxService.GetMonedasAsync();
                Metodopagos = await _auxService.GetMetodoPagoAsync();
                Cliente = await _auxService.GetSociosNegocioAsync();
                Cuentas = await _auxService.GetCuentasAsync();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error CargarComboBoxes TransaccionesViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        private async Task CargarSubTipos()
        {
            try
            {
                Subtipos = await _auxService.GetSubtiposFilterByIdAsync(TipoTransaccionIngresadoText);
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error CargarMonedas TransaccionesViewModel: " + Ex.Message);
            }
        }

        private async Task AgregarTransacciones(TransaccionesModel transacciones)
        {
            try
            {
                if (await _transaccionService.AddTransaccionAsync(transacciones))
                {
                    Transacciones.Add(transacciones);
                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error, ya existe una entrada asignada con este Folio.", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error AgregarTransaccionesDetalle TransaccionesViewModel: " + Ex.Message);
            }
        }

        private async Task EliminarTransacciones(int id)
        {
            try
            {
                if (await _transaccionService.DeleteTransaccionAsync(id))
                {
                    var transaccion = Transacciones.FirstOrDefault(p => p.Id_Transaccion == id);
                    if (transaccion != null)
                    {
                        Transacciones.Remove(transaccion);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error EliminarTransacciones IngresosViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public void Update()
        {
            try
            {
                ActualizarTransacciones((TransaccionesModel)OldTransaccion);
                Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");
                _navigationService.GoBackAsync();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Update TransaccionesViewModel: " + Ex.Message);
            }

        }

        private async Task ActualizarTransacciones(TransaccionesModel AntiguasTransacciones)
        {
            try
            {
                Console.WriteLine("EditFolioTransaccion: " + EditFolioTransaccion);
                Console.WriteLine("EditTipoTransaccion: " + EditTipoTransaccion);
                Console.WriteLine("EditMonedaTransaccion: " + EditMonedaTransaccion);
                Console.WriteLine("EditFechaTransaccion: " + EditFechaTransaccion);
                Console.WriteLine("EditClienteTransaccion: " + EditClienteTransaccion);
                Console.WriteLine("EditMetodoPagoTransaccion: " + EditMetodoPagoTransaccion);
                Console.WriteLine("EditBancoTransaccion: " + EditBancoTransaccion);
                Console.WriteLine("EditCuentaTransaccion: " + EditCuentaTransaccion);

                var id = IdTransaccionCeldaSeleccionada;
                var folio = EditFolioTransaccion;
                var tipo = EditTipoTransaccion;
                var subtipo = EditSubTipoTransaccion;
                var moneda = EditMonedaTransaccion;
                DateTime fecha = EditFechaTransaccion;
                var cliente = EditClienteTransaccion;
                var metodoPago = EditMetodoPagoTransaccion;
                var banco = EditBancoTransaccion;
                var cuenta = EditCuentaTransaccion;

                /*var date = fecha.ToString("yyyy-MM-dd",CultureInfo.InvariantCulture);
                var NewDate = Convert.ToDateTime(date);*/


                NewTransaccion = new TransaccionesModel(id, folio, tipo, subtipo, moneda, fecha, cliente, metodoPago, banco, cuenta)
                {
                    Id_Transaccion = id,
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

                if (await _transaccionService.UpdateTransaccionAsync((TransaccionesModel)NewTransaccion))
                {
                    //Remove Old Product
                    Transacciones.Remove(AntiguasTransacciones);

                    //Add new product
                    Transacciones.Add((TransaccionesModel)NewTransaccion);

                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error ActualizarTransacciones IngresosViewModel: " + Ex.Message);
            }
        }
        #endregion
    }
}
