using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
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

namespace DesktopAplicationCV.ViewModel
{
    public partial class FacturaVentaViewModel : BaseViewModel
    {
        #region Variables

        [ObservableProperty]
        private List<MonedaModel> _monedas;

        [ObservableProperty]
        private List<SocioNegocioModel> _cliente;

        private static object _oldFacturaVenta;
        private object NewFacturaVenta;

        private static int IdFactVentaCeldaSeleccionada;
        private int FolioFactVentaCeldaSeleccionada;
        private string ClienteFactVentaCeldaSeleccionada;
        private string DirDespachoFactVentaCeldaSeleccionada;
        private string MonedaFactVentaCeldaSeleccionada;
        private DateTime FechaFactVentaCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly AuxService _auxService;
        private readonly FacturaVentaService _factVentaService;

        [ObservableProperty]
        private int selectedIndex;

        private SocioNegocioModel _clienteSeleccionado;

        [ObservableProperty]
        private ObservableCollection<FacturaVentaModel> facturaVentaModels;

        private MonedaModel _monedaSeleccionado;

        private string _filterText;

        private int _folioFactVentaIngresado;
        private string _clienteFactVentaIngresado;
        private string _dirDespachoFactVentaIngresado;
        private string _monedaFactVentaIngresado;
        private DateTime _fechaFactVentaIngresado;

        //private int _editIdFactVenta;
        private int _editFolioFactVenta;
        private string _editClienteFactVenta;
        private string _editDirDespachoFactVenta;
        private string _editMonedaFactVenta;
        private DateTime _editFechaFactVenta;

        public ICommand CargarFactVentasCommand { get; }
        public ICommand AgregarFactVentasCommand { get; }
        public ICommand EliminarFactVentasCommand { get; }
        public ICommand ActualizarProductoCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Encapsulado

        public ObservableCollection<FacturaVentaModel> FacturaVentaInfoCollection
        {
            get { return facturaVentaModels; }
            set { facturaVentaModels = value; }
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

        public object OldFacturaVenta
        {
            get => _oldFacturaVenta;
            set
            {
                if (_oldFacturaVenta != value)
                {
                    _oldFacturaVenta = value;
                }
            }
        }

        public int FolioFactVentaIngresado
        {
            get => _folioFactVentaIngresado;
            set
            {
                if (_folioFactVentaIngresado != value)
                {
                    _folioFactVentaIngresado = value;
                    OnPropertyChanged(nameof(FolioFactVentaIngresado));
                }
            }
        }

        public string ClienteFactVentaIngresado
        {
            get => _clienteFactVentaIngresado;
            set
            {
                if (_clienteFactVentaIngresado != value)
                {
                    _clienteFactVentaIngresado = value;
                    OnPropertyChanged(nameof(ClienteFactVentaIngresado));
                }
            }
        }

        public string DirDespachoFactVentaIngresado
        {
            get => _dirDespachoFactVentaIngresado;
            set
            {
                if (_dirDespachoFactVentaIngresado != value)
                {
                    _dirDespachoFactVentaIngresado = value;
                    OnPropertyChanged(nameof(DirDespachoFactVentaIngresado));
                }
            }
        }

        public string MonedaFactVentaIngresado
        {
            get => _monedaFactVentaIngresado;
            set
            {
                if (_monedaFactVentaIngresado != value)
                {
                    _monedaFactVentaIngresado = value;
                    OnPropertyChanged(nameof(MonedaFactVentaIngresado));
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
                    MonedaFactVentaIngresado = value.Nombre.Trim();
                    EditMonedaFactVenta = value.Nombre.Trim();
                    OnPropertyChanged(nameof(MonedaSeleccionado));
                    OnPropertyChanged(nameof(MonedaFactVentaIngresado)); // Para actualizar la vista
                }
            }
        }

        public DateTime FechaFactVentaIngresado
        {
            get => _fechaFactVentaIngresado;
            set
            {
                if (_fechaFactVentaIngresado != value)
                {
                    _fechaFactVentaIngresado = value;
                    OnPropertyChanged(nameof(FechaFactVentaIngresado));
                }
            }
        }

        /*public int EditIdFactVenta
        {
            get => _editIdFactVenta;
            set
            {
                if (_editIdFactVenta != value)
                {
                    _editIdFactVenta = value;
                    OnPropertyChanged(nameof(EditIdFactVenta));
                }
            }
        }*/
        
        public int EditFolioFactVenta
        {
            get => _editFolioFactVenta;
            set
            {
                if (_editFolioFactVenta != value)
                {
                    _editFolioFactVenta = value;
                    OnPropertyChanged(nameof(EditFolioFactVenta));
                }
            }
        }

        public string EditClienteFactVenta
        {
            get => _editClienteFactVenta;
            set
            {
                if (_editClienteFactVenta != value)
                {
                    _editClienteFactVenta = value;
                    OnPropertyChanged(nameof(EditClienteFactVenta));
                }
            }
        }

        public string EditDirDespachoFactVenta
        {
            get => _editDirDespachoFactVenta;
            set
            {
                if (_editDirDespachoFactVenta != value)
                {
                    _editDirDespachoFactVenta = value;
                    OnPropertyChanged(nameof(EditDirDespachoFactVenta));
                }
            }
        }

        public string EditMonedaFactVenta
        {
            get => _editMonedaFactVenta;
            set
            {
                if (_editMonedaFactVenta != value)
                {
                    _editMonedaFactVenta = value;
                    OnPropertyChanged(nameof(EditMonedaFactVenta));
                }
            }
        }

        public DateTime EditFechaFactVenta
        {
            get => _editFechaFactVenta;
            set
            {
                if (_editFechaFactVenta != value)
                {
                    _editFechaFactVenta = value;
                    OnPropertyChanged(nameof(EditFechaFactVenta));
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
                    ClienteFactVentaIngresado = value.Nombre.Trim();
                    EditClienteFactVenta = value.Nombre.Trim();
                    OnPropertyChanged(nameof(ClienteSeleccionado));
                    OnPropertyChanged(nameof(ClienteFactVentaIngresado)); // Para actualizar la vista
                }
            }
        }

        #endregion

        #region Constructores

        public FacturaVentaViewModel(INavigationService navigationService, AuxService auxService)
        {
            _auxService = auxService;
            _factVentaService = new FacturaVentaService();
            facturaVentaModels = new ObservableCollection<FacturaVentaModel>();

            _navigationService = navigationService;
            CargarFactVentas();
            CargarComboBoxes();

            CeldaTocadaCommand = new Command<DataGridCellTappedEventArgs>(CeldaTocada);
        }

        #endregion

        #region Binding Methods 

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
                if (item is FacturaVentaModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Folio.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Cliente.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Direccion_Despacho.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Moneda.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Fecha.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        // Método que se ejecuta cuando se toca una celda
        private void CeldaTocada(DataGridCellTappedEventArgs e)
        {
            try
            {
                if (e.RowData is FacturaVentaModel facturaVentaModel)
                {
                    IdFactVentaCeldaSeleccionada = facturaVentaModel.Id_Factura_Venta;
                    FolioFactVentaCeldaSeleccionada = facturaVentaModel.Folio;
                    ClienteFactVentaCeldaSeleccionada = facturaVentaModel.Cliente;
                    DirDespachoFactVentaCeldaSeleccionada = facturaVentaModel.Direccion_Despacho;
                    MonedaFactVentaCeldaSeleccionada = facturaVentaModel.Moneda;
                    FechaFactVentaCeldaSeleccionada = facturaVentaModel.Fecha;
                    // Aquí puedes manejar la lógica de negocio sin tocar la vista
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error CeldaTocada FacturaVentaViewModel: " + ex.Message);
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
            CargarFactVentas();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    EliminarFactVentas(IdFactVentaCeldaSeleccionada);
                    CargarFactVentas();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
                Console.WriteLine("Error Eliminar FacturaVentaViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void Agregar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Factura_Ventas");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                Console.WriteLine("Error Agregar FacturaVentaViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void InsertarFactVenta()
        {
            try
            {
                if (FolioFactVentaIngresado != 0 && !string.IsNullOrEmpty(ClienteFactVentaIngresado) && !string.IsNullOrEmpty(DirDespachoFactVentaIngresado)
                && !string.IsNullOrEmpty(MonedaFactVentaIngresado))
                {
                    AgregarFactVentas(new FacturaVentaModel(IdFactVentaCeldaSeleccionada, FolioFactVentaIngresado, ClienteFactVentaIngresado, DirDespachoFactVentaIngresado
                        , MonedaFactVentaIngresado, FechaFactVentaIngresado));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
                }
            }
            catch(Exception Ex)
            { 
                Console.WriteLine("Error InsertarFactVenta FacturaVentaViewModel: " + Ex.Message);
            }
        }

        //private async Task NavigateToDetail()
        [RelayCommand]
        private async void Editar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    try
                    {
                        OldFacturaVenta = new FacturaVentaModel(IdFactVentaCeldaSeleccionada, FolioFactVentaCeldaSeleccionada, ClienteFactVentaCeldaSeleccionada,
                            DirDespachoFactVentaCeldaSeleccionada, MonedaFactVentaCeldaSeleccionada, FechaFactVentaCeldaSeleccionada)
                        {
                            Id_Factura_Venta = IdFactVentaCeldaSeleccionada,
                            Folio = FolioFactVentaCeldaSeleccionada,
                            Cliente = ClienteFactVentaCeldaSeleccionada,
                            Direccion_Despacho = DirDespachoFactVentaCeldaSeleccionada,
                            Moneda = MonedaFactVentaCeldaSeleccionada,
                            Fecha = FechaFactVentaCeldaSeleccionada
                        };

                        await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Factura_Ventas", OldFacturaVenta);
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
                Console.WriteLine("Error InsertarFactVenta FacturaVentaViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        private async void Detalles()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    await _navigationService.NavigateToAsync<NavigationViewModel>("Factura_Venta_Detalle", IdFactVentaCeldaSeleccionada);
                    //Application.Current.MainPage.DisplayAlert("Alerta", "Has seleccionado una Fila Valida", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Debe seleccionar una fila valida", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("ERROR: Detalles ViewModelFacturaVenta: " + Ex.Message);
            }
        }

        private async Task CargarFactVentas()
        {
            try
            {
                var facturaVentas = await _factVentaService.GetFactVentaAsync();
                facturaVentaModels.Clear();
                foreach (var factVentas in facturaVentas)
                {
                    facturaVentaModels.Add(factVentas);
                }
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error CargarFactVentas FacturaVentaViewModel: " + Ex.Message);
            }
        }

        private async Task CargarComboBoxes()
        {
            var tipo = "Cliente";
            try
            {
                Cliente = await _auxService.GetSocioNegociosFilterByIdAsync(tipo);
                Monedas = await _auxService.GetMonedasAsync();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error CargarComboBoxes FacturaVentaViewModel: " + Ex.Message);
            }
        }

        private async Task AgregarFactVentas(FacturaVentaModel facturaVentaModel)
        {
            try
            {
                if (await _factVentaService.AddFactVentaAsync(facturaVentaModel))
                {
                    facturaVentaModels.Add(facturaVentaModel);
                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error, ya existe una entrada asignada con este Folio.", "Ok");
                }
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error AgregarFactVentas FacturaVentaViewModel: " + Ex.Message);
            }
        }

        private async Task EliminarFactVentas(int id)
        {
            try
            {
                if (await _factVentaService.DeleteFactVentaAsync(id))
                {
                    var facturaVenta = facturaVentaModels.FirstOrDefault(p => p.Id_Factura_Venta == id);
                    if (facturaVenta != null)
                    {
                        facturaVentaModels.Remove(facturaVenta);
                    }
                }
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error EliminarFactVentas FacturaVentaViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public void Update()
        {
            try
            {
                ActualizarFactVentas((FacturaVentaModel)OldFacturaVenta);
                _navigationService.GoBackAsync();
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error Update FacturaVentaViewModel: " + Ex.Message);
            }
        }

        private async Task ActualizarFactVentas(FacturaVentaModel AntiguoFactVentas)
        {
            try
            {
                Console.WriteLine("EditFolioFactVenta: " + EditFolioFactVenta);
                Console.WriteLine("EditClienteFactVenta: " + EditClienteFactVenta);
                Console.WriteLine("EditDirDespachoFactVenta: " + EditDirDespachoFactVenta);
                Console.WriteLine("EditMonedaFactVenta: " + EditMonedaFactVenta);
                Console.WriteLine("EditFechaFactVenta: " + EditFechaFactVenta);

                var id = IdFactVentaCeldaSeleccionada;
                var folio = EditFolioFactVenta;
                var cliente = EditClienteFactVenta;
                var dirDespacho = EditDirDespachoFactVenta;
                var moneda = EditMonedaFactVenta;
                var fecha = EditFechaFactVenta;

                NewFacturaVenta = new FacturaVentaModel(id, folio, cliente, dirDespacho, moneda, fecha)
                {
                    Id_Factura_Venta = id,
                    Folio = folio,
                    Cliente = cliente,
                    Direccion_Despacho = dirDespacho,
                    Moneda = moneda,
                    Fecha = fecha
                };

                if (await _factVentaService.UpdateFactVentaAsync((FacturaVentaModel)NewFacturaVenta))
                {
                    //Remove Old
                    facturaVentaModels.Remove(AntiguoFactVentas);
                    //Add new
                    facturaVentaModels.Add((FacturaVentaModel)NewFacturaVenta);

                    Application.Current.MainPage.DisplayAlert("Alerta", "Se han modificado los datos correctamente. ", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error, no ha sido posible realizar la actualizacion.", "Ok");
                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine("Error ActualizarFactVentas FacturaVentaViewModel: " + Ex.Message);
            }
        }
        #endregion
    }
}
