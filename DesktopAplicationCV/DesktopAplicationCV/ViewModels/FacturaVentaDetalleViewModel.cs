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
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopAplicationCV.ViewModel
{
    public partial class FacturaVentaDetalleViewModel : BaseViewModel
    {
        #region Variables

        [ObservableProperty]
        private List<ProductosModel> _productos;

        private static int _idFacturaVenta;

        private const int MAX_INT = 2147483647; // Máximo permitido en SQL Server para INT

        private static object _oldFactVentaDetalle;
        private object NewFactVentaDetalle;

        private static int IdFactVentaDetalleSeleccionado;
        private int FolioFactVentaDetalleCeldaSeleccionada;
        private string CodProductoFactVentaDetalleCeldaSeleccionada;
        private int CantidadFactVentaDetalleCeldaSeleccionada;
        private int PrecioFactVentaDetalleCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly AuxService _auxService;
        private readonly FacturaVentaDetalleService _factVentaDetalleService;

        [ObservableProperty]
        private int selectedIndex;

        [ObservableProperty]
        private ObservableCollection<FacturaVentaDetalleModel> facturaVentaDetalleModels;

        private ProductosModel _productoSeleccionado;

        private string _filterText;

        private int _folioFactVentaDetalleIngresado;
        private string _codProductoFactVentaDetalleIngresado;

        private string _cantidadFactVentaDetalleFormateado;
        private string _precioFactVentaDetalleFormateado;

        //private int _cantidadFactVentaDetalleIngresado;
        //private long _precioFactVentaDetalleIngresado;

        private int _editfolioFactVentaDetalle;
        private string _editCodProductoFactVentaDetalle;
        private int _editCantidadFactVentaDetalle;
        private long _editPrecioFactVentaDetalle;

        public ICommand CargarFactVentaDetallesCommand { get; }
        public ICommand AgregarFactVentaDetalleCommand { get; }
        public ICommand EliminarFactVentaDetalleCommand { get; }
        public ICommand ActualizarFactVentaDetalleCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Encapsulado

        public ObservableCollection<FacturaVentaDetalleModel> FacturaVentaDetalleInfoCollection
        {
            get { return facturaVentaDetalleModels; }
            set { facturaVentaDetalleModels = value; }
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

        public object OldFactVentaDetalle
        {
            get => _oldFactVentaDetalle;
            set
            {
                if (_oldFactVentaDetalle != value)
                {
                    _oldFactVentaDetalle = value;
                }
            }
        }

        public int Id_Factura_Venta
        {
            get => _idFacturaVenta;
            set
            {
                if(_idFacturaVenta != value)
                {
                    _idFacturaVenta = value;
                    OnPropertyChanged(nameof(Id_Factura_Venta));
                }
            }
        }

        public ProductosModel ProductoSeleccionado
        {
            get => _productoSeleccionado;
            set
            {
                if (_productoSeleccionado != value)
                {
                    _productoSeleccionado = value;
                    CodProductoFactVentaDetalleIngresado = value.Codigo.Trim();
                    EditCodProductoFactVentaDetalle = value.Codigo.Trim();
                    OnPropertyChanged(nameof(ProductoSeleccionado));
                    OnPropertyChanged(nameof(CodProductoFactVentaDetalleIngresado)); // Para actualizar la vista
                }
            }
        }

        public int FolioFactVentaDetalleIngresado
        {
            get => _folioFactVentaDetalleIngresado;
            set
            {
                if (_folioFactVentaDetalleIngresado != value)
                {
                    _folioFactVentaDetalleIngresado = value;
                    OnPropertyChanged(nameof(FolioFactVentaDetalleIngresado));
                }
            }
        }

        public string CodProductoFactVentaDetalleIngresado
        {
            get => _codProductoFactVentaDetalleIngresado;
            set
            {
                if (_codProductoFactVentaDetalleIngresado != value)
                {
                    _codProductoFactVentaDetalleIngresado = value;
                    OnPropertyChanged(nameof(CodProductoFactVentaDetalleIngresado));
                }
            }
        }

        public string CantidadFactVentaDetalleFormateado
        {
            get => _cantidadFactVentaDetalleFormateado;
            set
            {
                if (_cantidadFactVentaDetalleFormateado != value)
                {
                    _cantidadFactVentaDetalleFormateado = value;
                    OnPropertyChanged(nameof(CantidadFactVentaDetalleFormateado));
                }
            }
        }

        public string PrecioFactVentaDetalleFormateado
        {
            get => _precioFactVentaDetalleFormateado;
            set
            {
                if (_precioFactVentaDetalleFormateado != value)
                {
                    _precioFactVentaDetalleFormateado = value;
                    OnPropertyChanged(nameof(PrecioFactVentaDetalleFormateado));
                }
            }
        }

        public int EditfolioFactVentaDetalle
        {
            get => _editfolioFactVentaDetalle;
            set
            {
                if (_editfolioFactVentaDetalle != value)
                {
                    _editfolioFactVentaDetalle = value;
                    OnPropertyChanged(nameof(EditfolioFactVentaDetalle));
                }
            }
        }

        public string EditCodProductoFactVentaDetalle
        {
            get => _editCodProductoFactVentaDetalle;
            set
            {
                if (_editCodProductoFactVentaDetalle != value)
                {
                    _editCodProductoFactVentaDetalle = value;
                    OnPropertyChanged(nameof(EditCodProductoFactVentaDetalle));
                }
            }
        }

        public int EditCantidadFactVentaDetalle
        {
            get => _editCantidadFactVentaDetalle;
            set
            {
                if (_editCantidadFactVentaDetalle != value)
                {
                    _editCantidadFactVentaDetalle = value;
                    OnPropertyChanged(nameof(EditCantidadFactVentaDetalle));
                }
            }
        }

        public long EditPrecioFactVentaDetalle
        {
            get => _editPrecioFactVentaDetalle;
            set
            {
                if (_editPrecioFactVentaDetalle != value)
                {
                    _editPrecioFactVentaDetalle = value;
                    OnPropertyChanged(nameof(EditPrecioFactVentaDetalle));
                }
            }
        }

        public int CantidadFactVentaDetalleIngresado =>
    int.TryParse(CantidadFactVentaDetalleFormateado, NumberStyles.Currency, CultureInfo.CurrentCulture, out var resultado)
        ? (int)resultado
        : 0;

        public long PrecioFactVentaDetalleIngresado =>
    int.TryParse(PrecioFactVentaDetalleFormateado, NumberStyles.Currency, CultureInfo.CurrentCulture, out var resultado)
        ? (int)resultado
        : 0;

        #endregion

        #region Constructores

        public FacturaVentaDetalleViewModel(INavigationService navigationService, AuxService auxService)
        {
            FacturaVentaDetalleInfoCollection = new ObservableCollection<FacturaVentaDetalleModel>();
            CeldaTocadaCommand = new Command<DataGridCellTappedEventArgs>(CeldaTocada);

            _factVentaDetalleService = new FacturaVentaDetalleService();
            _navigationService = navigationService;
            _auxService = auxService;

            CargarFactVentaDetalle();
            CargarComboBoxes();
        }

        #endregion

        #region Metodos 

        [RelayCommand]
        public async void Volver()
        {
            try
            {
                facturaVentaDetalleModels.Clear();
                Id_Factura_Venta = 0;
                await _navigationService.GoBackAsync();
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error. ", "Ok");
                Console.WriteLine("Error Volver FcaturVentaDetalleViewsModel: " + Ex.Message);
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
                if (item is FacturaVentaDetalleModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Folio.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Codigo_Producto.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Cantidad.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Precio.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        // Método que se ejecuta cuando se toca una celda
        private void CeldaTocada(DataGridCellTappedEventArgs e)
        {
            try
            {
                if (e.RowData is FacturaVentaDetalleModel facturaVentaDetalleModel)
                {
                    IdFactVentaDetalleSeleccionado = facturaVentaDetalleModel.Id_Factura_Venta_Detalle;
                    FolioFactVentaDetalleCeldaSeleccionada = facturaVentaDetalleModel.Folio;
                    CodProductoFactVentaDetalleCeldaSeleccionada = facturaVentaDetalleModel.Codigo_Producto;
                    CantidadFactVentaDetalleCeldaSeleccionada = facturaVentaDetalleModel.Cantidad;
                    PrecioFactVentaDetalleCeldaSeleccionada = facturaVentaDetalleModel.Precio;
                    // Aquí puedes manejar la lógica de negocio sin tocar la vista
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error CeldaTocada FacturaVentaDetalleViewModel" + ex.Message);
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
            CargarFactVentaDetalle();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    EliminarFactVentaDetalle(IdFactVentaDetalleSeleccionado);
                    CargarFactVentaDetalle();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
                Console.WriteLine("Error Eliminar FacturaVentaDetalleViewModel" + Ex.Message);
            }
        }

        private async Task CargarComboBoxes()
        {
            try
            {
                Productos = await _auxService.GetProductosAsync();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error CargarComboBoxes FacturaVentaViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void Agregar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Factura_Venta_Detalle");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                Console.WriteLine("Error Agregar FacturaVentaDetalleViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void InsertarFactVentaDetalle()
        {
            try
            {
                if (Id_Factura_Venta != 0 && FolioFactVentaDetalleIngresado != 0 && !string.IsNullOrEmpty(CodProductoFactVentaDetalleIngresado)
                && CantidadFactVentaDetalleIngresado != 0 && PrecioFactVentaDetalleIngresado != 0)
                {
                    AgregarFactVentaDetalle(new FacturaVentaDetalleModel(0, Id_Factura_Venta, FolioFactVentaDetalleIngresado, CodProductoFactVentaDetalleIngresado,
                        CantidadFactVentaDetalleIngresado, (int)PrecioFactVentaDetalleIngresado));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
                }

            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Agregar InsertarFactVentaDetalle: " + Ex.Message);
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
                        OldFactVentaDetalle = new FacturaVentaDetalleModel(IdFactVentaDetalleSeleccionado, Id_Factura_Venta, FolioFactVentaDetalleCeldaSeleccionada,
                            CodProductoFactVentaDetalleCeldaSeleccionada, CantidadFactVentaDetalleCeldaSeleccionada, PrecioFactVentaDetalleCeldaSeleccionada)
                        {
                            Folio = FolioFactVentaDetalleCeldaSeleccionada,
                            Codigo_Producto = CodProductoFactVentaDetalleCeldaSeleccionada,
                            Cantidad = CantidadFactVentaDetalleCeldaSeleccionada,
                            Precio = PrecioFactVentaDetalleCeldaSeleccionada
                        };

                        await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Factura_Venta_Detalle", OldFactVentaDetalle);
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
                Console.WriteLine("Error Editar InsertarFactVentaDetalle: " + Ex.Message);
            }
        }

        private async Task CargarFactVentaDetalle()
        {
            try
            {
                var factVentaDetalles = await _factVentaDetalleService.GetFactVentaDetalleFilterByIdAsync(Id_Factura_Venta);
                facturaVentaDetalleModels.Clear();
                foreach (var factVentaDetalle in factVentaDetalles)
                {
                    facturaVentaDetalleModels.Add(factVentaDetalle);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error CargarFactVentaDetalle InsertarFactVentaDetalle: " + Ex.Message);
            }
        }

        private async Task AgregarFactVentaDetalle(FacturaVentaDetalleModel facturaVentaDetalleModel)
        {
            try
            {
                if (!(PrecioFactVentaDetalleIngresado > MAX_INT))
                {
                    if (await _factVentaDetalleService.AddFactVentaDetalleAsync(facturaVentaDetalleModel))
                    {
                        facturaVentaDetalleModels.Add(facturaVentaDetalleModel);
                        Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente", "Ok");
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error, ya existe una entrada asignada con este Folio.", "Ok");
                    }
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", $"El valor no puede superar {MAX_INT}", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error AgregarFactVentaDetalle InsertarFactVentaDetalle: " + Ex.Message);
            }
        }

        private async Task EliminarFactVentaDetalle(int id)
        {
            try
            {
                if (await _factVentaDetalleService.DeleteFactVentaDetalleAsync(id))
                {
                    var facturaVentaDetalle = facturaVentaDetalleModels.FirstOrDefault(p => p.Id_Factura_Venta_Detalle == id);
                    if (facturaVentaDetalle != null)
                    {
                        facturaVentaDetalleModels.Remove(facturaVentaDetalle);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error EliminarFactVentaDetalle InsertarFactVentaDetalle: " + Ex.Message);
            }
        }

        [RelayCommand]
        public void Update()
        {
            try
            {
                ActualizarFactVentaDetalle((FacturaVentaDetalleModel)OldFactVentaDetalle);
                _navigationService.GoBackAsync();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Update InsertarFactVentaDetalle: " + Ex.Message);
            }
        }

        private async Task ActualizarFactVentaDetalle(FacturaVentaDetalleModel AntiguoFactVentaDetalle)
        {
            try
            {
                Console.WriteLine("EditfolioFactVentaDetalle: " + EditfolioFactVentaDetalle);
                Console.WriteLine("EditCodProductoFactVentaDetalle: " + EditCodProductoFactVentaDetalle);
                Console.WriteLine("EditCantidadFactVentaDetalle: " + EditCantidadFactVentaDetalle);
                Console.WriteLine("EditPrecioFactVentaDetalle: " + EditPrecioFactVentaDetalle);

                var id = IdFactVentaDetalleSeleccionado;
                var idFacturaVenta = Id_Factura_Venta;
                var folio = EditfolioFactVentaDetalle;
                var codigo = EditCodProductoFactVentaDetalle;
                var cantidad = CantidadFactVentaDetalleIngresado;
                var precio = (int)PrecioFactVentaDetalleIngresado;

                NewFactVentaDetalle = new FacturaVentaDetalleModel(id, idFacturaVenta, folio, codigo, cantidad, precio)
                {
                    Id_Factura_Venta_Detalle = id,
                    Folio = folio,
                    Codigo_Producto = codigo,
                    Cantidad = cantidad,
                    Precio = precio
                };

                if (!(EditPrecioFactVentaDetalle > MAX_INT))
                {
                    if (await _factVentaDetalleService.UpdateFactVentaDetalleAsync((FacturaVentaDetalleModel)NewFactVentaDetalle))
                    {
                        //Remove Old Product
                        facturaVentaDetalleModels.Remove(AntiguoFactVentaDetalle);
                        //Add new product
                        facturaVentaDetalleModels.Add((FacturaVentaDetalleModel)NewFactVentaDetalle);
                        Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la actualizacion. ", "Ok");
                    }
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", $"El valor no puede superar {MAX_INT}", "Ok");
                }

            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error ActualizarFactVentaDetalle InsertarFactVentaDetalle: " + Ex.Message);
            }
        }
        #endregion
    }
}
