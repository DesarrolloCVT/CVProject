using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using Syncfusion.Maui.DataGrid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Services.Maps;

namespace DesktopAplicationCV.ViewModel
{
    public partial class IngresosViewModel : BaseViewModel
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

        private static object _oldIngreso;
        private object NewIngreso;

        private static int IdIngresoCeldaSeleccionada;
        private int FolioIngresoCeldaSeleccionada;
        private string TipoIngresoCeldaSeleccionada;
        private string SubTipoIngresoCeldaSeleccionada;
        private string MonedaIngresoCeldaSeleccionada;
        private DateTime FechaIngresoCeldaSeleccionada;
        private string ClienteIngresoCeldaSeleccionada;
        private string MetodoPagoIngresoCeldaSeleccionada;
        private string BancoIngresoCeldaSeleccionada;
        private string CuentaIngresoCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly ApiService _apiService;
        private readonly IngresosService _ingresoService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<IngresosModel> Ingresos;

        private string _filterText;
        private string _tituloPagina;

        private int _folioIngresosIngresadoText;
        private string _tipoIngresosIngresadoText;
        private string _subTipoIngresosIngresadoText;
        private string _monedaIngresosIngresadoText;
        private DateTime _fechaIngresosIngresadoText;
        private string _clienteIngresosIngresadoText;
        private string _metodoPagoIngresosIngresadoText;
        private string _bancoIngresosIngresadoText;
        private string _cuentaIngresosIngresadoText;


        private int _editFolioIngresos;
        private string _editTipoIngresos;
        private string _editSubTipoIngresos;
        private string _editMonedaIngresos;
        private DateTime _editFechaIngresos;
        private string _editClienteIngresos;
        private string _editMetodoPagoIngresos;
        private string _editBancoIngresos;
        private string _editCuentaIngresos;

        public ICommand CargarIngresosCommand { get; }
        public ICommand AgregarIngresoCommand { get; }
        public ICommand EliminarIngresoCommand { get; }
        public ICommand ActualizarIngresoCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Encapsulado

        public ObservableCollection<IngresosModel> IngresosInfoCollection
        {
            get { return Ingresos; }
            set { Ingresos = value; }
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

        public object OldIngreso
        {
            get => _oldIngreso;
            set
            {
                if (_oldIngreso != value)
                {
                    _oldIngreso = value;
                }
            }
        }

        public int FolioIngresosIngresadoText
        {
            get => _folioIngresosIngresadoText;
            set
            {
                if (_folioIngresosIngresadoText != value)
                {
                    _folioIngresosIngresadoText = value;
                    OnPropertyChanged(nameof(FolioIngresosIngresadoText));
                }
            }
        }

        public int EditFolioIngresos
        {
            get => _editFolioIngresos;
            set
            {
                if (_editFolioIngresos != value)
                {
                    _editFolioIngresos = value;
                    OnPropertyChanged(nameof(EditFolioIngresos));
                }
            }
        }

        public string TipoIngresosIngresadoText
        {
            get => _tipoIngresosIngresadoText;
            set
            {
                if (_tipoIngresosIngresadoText != value)
                {
                    _tipoIngresosIngresadoText = value;
                    OnPropertyChanged(nameof(TipoIngresosIngresadoText));
                }
            }
        }

        public string EditTipoIngresos
        {
            get => _editTipoIngresos;
            set
            {
                if (_editTipoIngresos != value)
                {
                    _editTipoIngresos = value;
                    OnPropertyChanged(nameof(EditTipoIngresos));
                }
            }
        }

        public string SubTipoIngresosIngresadoText
        {
            get => _subTipoIngresosIngresadoText;
            set
            {
                if (_subTipoIngresosIngresadoText != value)
                {
                    _subTipoIngresosIngresadoText = value;
                    OnPropertyChanged(nameof(SubTipoIngresosIngresadoText));
                }
            }
        }

        public string EditSubTipoIngresos
        {
            get => _editSubTipoIngresos;
            set
            {
                if (_editSubTipoIngresos != value)
                {
                    _editSubTipoIngresos = value;
                    OnPropertyChanged(nameof(EditSubTipoIngresos));
                }
            }
        }

        public string MonedaIngresosIngresadoText
        {
            get => _monedaIngresosIngresadoText;
            set
            {
                if (_monedaIngresosIngresadoText != value)
                {
                    _monedaIngresosIngresadoText = value;
                    OnPropertyChanged(nameof(MonedaIngresosIngresadoText));
                }
            }
        }

        public string EditMonedaIngresos
        {
            get => _editMonedaIngresos;
            set
            {
                if (_editMonedaIngresos != value)
                {
                    _editMonedaIngresos = value;
                    OnPropertyChanged(nameof(EditMonedaIngresos));
                }
            }
        }

        public DateTime FechaIngresosIngresadoText
        {
            get => _fechaIngresosIngresadoText;
            set
            {
                if (_fechaIngresosIngresadoText != value)
                {
                    _fechaIngresosIngresadoText = value;
                    OnPropertyChanged(nameof(FechaIngresosIngresadoText));
                }
            }
        }

        public DateTime EditFechaIngresos
        {
            get => _editFechaIngresos;
            set
            {
                if (_editFechaIngresos != value)
                {
                    _editFechaIngresos = value;
                    OnPropertyChanged(nameof(EditFechaIngresos));
                }
            }
        }

        public string ClienteIngresosIngresadoText
        {
            get => _clienteIngresosIngresadoText;
            set
            {
                if (_clienteIngresosIngresadoText != value)
                {
                    _clienteIngresosIngresadoText = value;
                    OnPropertyChanged(nameof(ClienteIngresosIngresadoText));
                }
            }
        }

        public string EditClienteIngresos
        {
            get => _editClienteIngresos;
            set
            {
                if (_editClienteIngresos != value)
                {
                    _editClienteIngresos = value;
                    OnPropertyChanged(nameof(EditClienteIngresos));
                }
            }
        }

        public string MetodoPagoIngresosIngresadoText
        {
            get => _metodoPagoIngresosIngresadoText;
            set
            {
                if (_metodoPagoIngresosIngresadoText != value)
                {
                    _metodoPagoIngresosIngresadoText = value;
                    OnPropertyChanged(nameof(MetodoPagoIngresosIngresadoText));
                }
            }
        }

        public string EditMetodoPagoIngresos
        {
            get => _editMetodoPagoIngresos;
            set
            {
                if (_editMetodoPagoIngresos != value)
                {
                    _editMetodoPagoIngresos = value;
                    OnPropertyChanged(nameof(EditMetodoPagoIngresos));
                }
            }
        }

        public string BancoIngresosIngresadoText
        {
            get => _bancoIngresosIngresadoText;
            set
            {
                if (_bancoIngresosIngresadoText != value)
                {
                    _bancoIngresosIngresadoText = value;
                    OnPropertyChanged(nameof(BancoIngresosIngresadoText));
                }
            }
        }

        public string EditBancoIngresos
        {
            get => _editBancoIngresos;
            set
            {
                if (_editBancoIngresos != value)
                {
                    _editBancoIngresos = value;
                    OnPropertyChanged(nameof(EditBancoIngresos));
                }
            }
        }

        public string CuentaIngresosIngresadoText
        {
            get => _cuentaIngresosIngresadoText;
            set
            {
                if (_cuentaIngresosIngresadoText != value)
                {
                    _cuentaIngresosIngresadoText = value;
                    OnPropertyChanged(nameof(CuentaIngresosIngresadoText));
                }
            }
        }

        public string EditCuentaIngresos
        {
            get => _editCuentaIngresos;
            set
            {
                if (_editCuentaIngresos != value)
                {
                    _editCuentaIngresos = value;
                    OnPropertyChanged(nameof(EditCuentaIngresos));
                }
            }
        }

        #endregion

        #region Constructores

        public IngresosViewModel(INavigationService navigationService, ApiService apiService)
        {   
            _apiService = apiService;
            _ingresoService = new IngresosService();
            Ingresos = new ObservableCollection<IngresosModel>();

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
                if (item is IngresosModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Id_Ingreso.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
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
                if (e.RowData is IngresosModel ingresos)
                {
                    IdIngresoCeldaSeleccionada = ingresos.Id_Ingreso;
                    FolioIngresoCeldaSeleccionada = ingresos.Folio;
                    TipoIngresoCeldaSeleccionada = ingresos.Tipo_Transaccion;
                    SubTipoIngresoCeldaSeleccionada = ingresos.Subtipo_Transaccion;
                    MonedaIngresoCeldaSeleccionada = ingresos.Moneda;
                    FechaIngresoCeldaSeleccionada = ingresos.Fecha;
                    ClienteIngresoCeldaSeleccionada = ingresos.Cliente;
                    MetodoPagoIngresoCeldaSeleccionada = ingresos.Metodo_Pago;
                    BancoIngresoCeldaSeleccionada = ingresos.Banco;
                    CuentaIngresoCeldaSeleccionada = ingresos.Cuenta;
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
            CargarGrillaIngresos();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    EliminarIngresos(FolioIngresoCeldaSeleccionada);
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
                Console.WriteLine("Error Eliminar IngresosViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void Agregar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Ingresos");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                Console.WriteLine("Error Agregar IngresosViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void InsertarIngresos()
        {
            try
            {
                if (FolioIngresosIngresadoText != 0 && !string.IsNullOrEmpty(TipoIngresosIngresadoText) && !string.IsNullOrEmpty(MonedaIngresosIngresadoText)
                && !string.IsNullOrEmpty(ClienteIngresosIngresadoText) && !string.IsNullOrEmpty(MetodoPagoIngresosIngresadoText)
                && !string.IsNullOrEmpty(BancoIngresosIngresadoText) && !string.IsNullOrEmpty(CuentaIngresosIngresadoText))
                {
                    AgregarIngresos(new IngresosModel(IdIngresoCeldaSeleccionada, FolioIngresosIngresadoText, TipoIngresosIngresadoText, SubTipoIngresosIngresadoText, MonedaIngresosIngresadoText,
                        FechaIngresosIngresadoText, ClienteIngresosIngresadoText, MetodoPagoIngresosIngresadoText, BancoIngresosIngresadoText,
                        CuentaIngresosIngresadoText));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
                }
            }
            catch(Exception Ex)
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
                        OldIngreso = new IngresosModel(IdIngresoCeldaSeleccionada, FolioIngresoCeldaSeleccionada, TipoIngresoCeldaSeleccionada, SubTipoIngresoCeldaSeleccionada, MonedaIngresoCeldaSeleccionada,
                            FechaIngresoCeldaSeleccionada, ClienteIngresoCeldaSeleccionada, MetodoPagoIngresoCeldaSeleccionada, BancoIngresoCeldaSeleccionada,
                            CuentaIngresoCeldaSeleccionada)
                        {
                            Id_Ingreso = IdIngresoCeldaSeleccionada,
                            Folio = FolioIngresoCeldaSeleccionada,
                            Tipo_Transaccion = TipoIngresoCeldaSeleccionada,
                            Subtipo_Transaccion = SubTipoIngresoCeldaSeleccionada,
                            Moneda = MonedaIngresoCeldaSeleccionada,
                            Fecha = FechaIngresoCeldaSeleccionada,
                            Cliente = ClienteIngresoCeldaSeleccionada,
                            Metodo_Pago = MetodoPagoIngresoCeldaSeleccionada,
                            Banco = BancoIngresoCeldaSeleccionada,
                            Cuenta = CuentaIngresoCeldaSeleccionada
                        };

                        await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Ingresos", OldIngreso);
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
            catch(Exception Ex)
            {
                Console.WriteLine("Error Editar IngresosViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        private async void Detalles()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    await _navigationService.NavigateToAsync<NavigationViewModel>("Ingresos_Detalle", IdIngresoCeldaSeleccionada);
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
                var ingresos = await _ingresoService.GetIngresosAsync();
                Ingresos.Clear();
                foreach (var ingreso in ingresos)
                {
                    Ingresos.Add(ingreso);
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
                /*Bancos = await _apiService.GetBancosAsync();
                Monedas = await _apiService.GetMonedasAsync();
                Metodopagos = await _apiService.GetMetodoPagoAsync();
                Subtipos = await _ingresoService.GetSubtiposFilterByIdAsync("ingreso");*/
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error CargarComboBoxes IngresosViewModel: " + Ex.Message);
            }
        }

        private async Task AgregarIngresos(IngresosModel ingreso)
        {
            try
            {
                if (await _ingresoService.AddIngresoAsync(ingreso))
                {
                    Ingresos.Add(ingreso);
                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error, ya existe una entrada asignada con este Folio.", "Ok");
                }
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error AgregarTransaccionesDetalle IngresosViewModel: " + Ex.Message);
            }
        }

        private async Task EliminarIngresos(int folio)
        {
            try
            {
                if (await _ingresoService.DeleteIngresoAsync(folio))
                {
                    var ingreso = Ingresos.FirstOrDefault(p => p.Folio == folio);
                    if (ingreso != null)
                    {
                        Ingresos.Remove(ingreso);
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
                ActualizarIngresos((IngresosModel)OldIngreso);
                Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");
                _navigationService.GoBackAsync();
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error Update IngresosViewModel: " + Ex.Message);
            }
            
        }

        private async Task ActualizarIngresos(IngresosModel AntiguoIngreso)
        {
            try
            {
                Console.WriteLine("EditFolioIngresos: " + EditFolioIngresos);
                Console.WriteLine("EditTipoIngresos: " + EditTipoIngresos);
                Console.WriteLine("EditMonedaIngresos: " + EditMonedaIngresos);
                Console.WriteLine("EditFechaIngresos: " + EditFechaIngresos);
                Console.WriteLine("EditClienteIngresos: " + EditClienteIngresos);
                Console.WriteLine("EditMetodoPagoIngresos: " + EditMetodoPagoIngresos);
                Console.WriteLine("EditBancoIngresos: " + EditBancoIngresos);
                Console.WriteLine("EditCuentaIngresos: " + EditCuentaIngresos);

                var id = IdIngresoCeldaSeleccionada;
                var folio = EditFolioIngresos;
                var tipo = EditTipoIngresos;
                var subtipo = EditSubTipoIngresos;
                var moneda = EditMonedaIngresos;
                DateTime fecha = EditFechaIngresos;
                var cliente = EditClienteIngresos;
                var metodoPago = EditMetodoPagoIngresos;
                var banco = EditBancoIngresos;
                var cuenta = EditCuentaIngresos;

                /*var date = fecha.ToString("yyyy-MM-dd",CultureInfo.InvariantCulture);
                var NewDate = Convert.ToDateTime(date);*/


                NewIngreso = new IngresosModel(id, folio, tipo, subtipo, moneda, fecha, cliente, metodoPago, banco, cuenta)
                {
                    Id_Ingreso = id,
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

                if (await _ingresoService.UpdateIngresoAsync((IngresosModel)NewIngreso))
                {
                    //Remove Old Product
                    Ingresos.Remove(AntiguoIngreso);

                    //Add new product
                    Ingresos.Add((IngresosModel)NewIngreso);

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
