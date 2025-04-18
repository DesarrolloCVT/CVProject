﻿using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class FacturaCompraDetalleViewModel : BaseViewModel
    {
        #region Variables

        [ObservableProperty]
        private List<ProductosModel> _productos;

        public static int _idFacturaCompra;

        private const int MAX_INT = 2147483647; // Máximo permitido en SQL Server para INT

        private static object _oldFacturaCompraDetalle;
        private object NewFacturaCompraDetalle;

        private static int IdFactCompraDetalleCeldaSeleccionada;
        private int FolioFactCompraDetalleCeldaSeleccionada;
        private string CodigoProdFactCompraDetalleCeldaSeleccionada;
        private int CantidadFactCompraDetalleCeldaSeleccionada;
        private int PrecioFactCompraDetalleCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly AuxService _auxService;
        private readonly FacturaCompraDetalleService _factComprDetalleService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<FacturaCompraDetalleModel> FacturaCompraDetalleModels;

        private ProductosModel _productoSeleccionado;

        private string _filterText;

        private int _folioFactCompraDetalleIngresadoText;
        private string _codigoProdFactCompraDetalleIngresadoText;
        private string _cantidadFactCompraDetalleFormateado;
        private string _precioFactCompraDetalleFormateado;

        /*private int _cantidadFactCompraDetalleIngresadoText;
        private long _precioFactCompraDetalleIngresadoText;*/



        private int _editIdFactCompraDetalle;
        private int _editFolioFactCompraDetalle;
        private string _editCodigoProdFactCompraDetalle;
        private int _editCantidadFactCompraDetalle;
        private long _editPrecioFactCompraDetalle;

        public ICommand CargarFactCompraDetallesCommand { get; }
        public ICommand AgregarFactCompraDetalleCommand { get; }
        public ICommand EliminarFactCompraDetalleCommand { get; }
        public ICommand ActualizarFactCompraDetalleCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Encapsulado

        public ObservableCollection<FacturaCompraDetalleModel> FacturaCompraDetalleInfoCollection
        {
            get { return FacturaCompraDetalleModels; }
            set { FacturaCompraDetalleModels = value; }
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

        public object OldFacturaCompraDetalle
        {
            get => _oldFacturaCompraDetalle;
            set
            {
                if (_oldFacturaCompraDetalle != value)
                {
                    _oldFacturaCompraDetalle = value;
                }
            }
        }

        public int Id_Factura_Compra
        {
            get => _idFacturaCompra;
            set
            {
                if (_idFacturaCompra != value)
                {
                    _idFacturaCompra = value;
                    OnPropertyChanged(nameof(Id_Factura_Compra));
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
                    CodigoProdFactCompraDetalleIngresado = value.Codigo.Trim();
                    EditCodigoProdFactCompraDetalle = value.Codigo.Trim();
                    OnPropertyChanged(nameof(ProductoSeleccionado));
                    OnPropertyChanged(nameof(CodigoProdFactCompraDetalleIngresado)); // Para actualizar la vista
                }
            }
        }

        public int FolioFactCompraDetalleIngresado
        {
            get => _folioFactCompraDetalleIngresadoText;
            set
            {
                if (_folioFactCompraDetalleIngresadoText != value)
                {
                    _folioFactCompraDetalleIngresadoText = value;
                    OnPropertyChanged(nameof(FolioFactCompraDetalleIngresado));
                }
            }
        }

        public int EditIdFactCompraDetalle
        {
            get => _editIdFactCompraDetalle;
            set
            {
                if (_editIdFactCompraDetalle != value)
                {
                    _editIdFactCompraDetalle = value;
                    OnPropertyChanged(nameof(EditIdFactCompraDetalle));
                }
            }
        }

        public int EditFolioFactCompraDetalle
        {
            get => _editFolioFactCompraDetalle;
            set
            {
                if (_editFolioFactCompraDetalle != value)
                {
                    _editFolioFactCompraDetalle = value;
                    OnPropertyChanged(nameof(EditFolioFactCompraDetalle));
                }
            }
        }

        public string CodigoProdFactCompraDetalleIngresado
        {
            get => _codigoProdFactCompraDetalleIngresadoText;
            set
            {
                if (_codigoProdFactCompraDetalleIngresadoText != value)
                {
                    _codigoProdFactCompraDetalleIngresadoText = value;
                    OnPropertyChanged(nameof(CodigoProdFactCompraDetalleIngresado));
                }
            }
        }

        public string EditCodigoProdFactCompraDetalle
        {
            get => _editCodigoProdFactCompraDetalle;
            set
            {
                if (_editCodigoProdFactCompraDetalle != value)
                {
                    _editCodigoProdFactCompraDetalle = value;
                    OnPropertyChanged(nameof(EditCodigoProdFactCompraDetalle));
                }
            }
        }

        /*public int CantidadFactCompraDetalleIngresado
        {
            get => _cantidadFactCompraDetalleIngresadoText;
            set
            {
                if (_cantidadFactCompraDetalleIngresadoText != value)
                {
                    _cantidadFactCompraDetalleIngresadoText = value;
                    OnPropertyChanged(nameof(CantidadFactCompraDetalleIngresado));
                }
            }
        }*/

        public string CantidadFactCompraDetalleFormateado
        {
            get => _cantidadFactCompraDetalleFormateado;
            set
            {
                if (_cantidadFactCompraDetalleFormateado != value)
                {
                    _cantidadFactCompraDetalleFormateado = value;
                    OnPropertyChanged(nameof(CantidadFactCompraDetalleFormateado));
                }
            }
        }

        public int EditCantidadFactCompraDetalle
        {
            get => _editCantidadFactCompraDetalle;
            set
            {
                if (_editCantidadFactCompraDetalle != value)
                {
                    _editCantidadFactCompraDetalle = value;
                    OnPropertyChanged(nameof(EditCantidadFactCompraDetalle));
                }
            }
        }

        /*public long PrecioFactCompraDetalleIngresado
        {
            get => _precioFactCompraDetalleIngresadoText;
            set
            {
                if (_precioFactCompraDetalleIngresadoText != value)
                {
                    _precioFactCompraDetalleIngresadoText = value;
                    OnPropertyChanged(nameof(PrecioFactCompraDetalleIngresado));
                }
            }
        }*/

        public string PrecioFactCompraDetalleFormateado
        {
            get => _precioFactCompraDetalleFormateado;
            set
            {
                if (_precioFactCompraDetalleFormateado != value)
                {
                    _precioFactCompraDetalleFormateado = value;
                    OnPropertyChanged(nameof(PrecioFactCompraDetalleFormateado));
                }
            }
        }

        public long EditPrecioFactCompraDetalle
        {
            get => _editPrecioFactCompraDetalle;
            set
            {
                if (_editPrecioFactCompraDetalle != value)
                {
                    _editPrecioFactCompraDetalle = value;
                    OnPropertyChanged(nameof(EditPrecioFactCompraDetalle));
                }
            }
        }

        public int CantidadFactCompraDetalleIngresado =>
    int.TryParse(CantidadFactCompraDetalleFormateado, NumberStyles.Currency, CultureInfo.CurrentCulture, out var resultado)
        ? (int)resultado
        : 0;

        public long PrecioFactCompraDetalleIngresado =>
    int.TryParse(PrecioFactCompraDetalleFormateado, NumberStyles.Currency, CultureInfo.CurrentCulture, out var resultado)
        ? (int)resultado
        : 0;

        #endregion

        #region Constructores

        public FacturaCompraDetalleViewModel(INavigationService navigationService, AuxService auxService)
        {
            FacturaCompraDetalleInfoCollection = new ObservableCollection<FacturaCompraDetalleModel>();
            CeldaTocadaCommand = new Command<DataGridCellTappedEventArgs>(CeldaTocada);

            

            _auxService = auxService;
            _factComprDetalleService = new FacturaCompraDetalleService();
            _navigationService = navigationService;

            CargarFactCompraDetalles();
            CargarComboBoxes();
        }

        #endregion

        #region Metodos 

        [RelayCommand]
        public async void Volver()
        {
            try
            {
                FacturaCompraDetalleModels.Clear();
                Id_Factura_Compra = 0;
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
                if (item is FacturaCompraDetalleModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Id_Factura_Compra_Detalle.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
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
                if (e.RowData is FacturaCompraDetalleModel facturaCompraModel)
                {
                    IdFactCompraDetalleCeldaSeleccionada = facturaCompraModel.Id_Factura_Compra_Detalle;
                    FolioFactCompraDetalleCeldaSeleccionada = facturaCompraModel.Folio;
                    CodigoProdFactCompraDetalleCeldaSeleccionada = facturaCompraModel.Codigo_Producto;
                    CantidadFactCompraDetalleCeldaSeleccionada = facturaCompraModel.Cantidad;
                    PrecioFactCompraDetalleCeldaSeleccionada = facturaCompraModel.Precio;
                    // Aquí puedes manejar la lógica de negocio sin tocar la vista
                }
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error CeldaTocada FacturaCompraDetalleViewModel: " + Ex.Message);
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
            CargarFactCompraDetalles();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    //EliminarFactCompraDetalle(FolioFactCompraDetalleCeldaSeleccionada);
                    EliminarFactCompraDetalle(IdFactCompraDetalleCeldaSeleccionada);
                    CargarFactCompraDetalles();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
                Console.WriteLine("Error Eliminar FacturaCompraDetalleViewModel: " + Ex.Message);
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
                Console.WriteLine("Error CargarComboBoxes FacturaCompraViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void Agregar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Factura_Compras_Detalle");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                Console.WriteLine("Error Agregar FacturaCompraDetalleViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void InsertarFactCompraDetalle()
        {
            try
            {
                if (FolioFactCompraDetalleIngresado != 0 && !string.IsNullOrEmpty(CodigoProdFactCompraDetalleIngresado)
                && CantidadFactCompraDetalleIngresado != 0 && PrecioFactCompraDetalleIngresado != 0)
                {
                    AgregarFactCompraDetalle(new FacturaCompraDetalleModel(0, Id_Factura_Compra, FolioFactCompraDetalleIngresado, CodigoProdFactCompraDetalleIngresado,
                        CantidadFactCompraDetalleIngresado, (int)PrecioFactCompraDetalleIngresado));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
                }
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error InsertarFactCompraDetalle FacturaCompraDetalleViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        private async void Editar()
        {
            if (selectedIndex >= 0)
            {
                try
                {
                    OldFacturaCompraDetalle = new FacturaCompraDetalleModel(IdFactCompraDetalleCeldaSeleccionada, Id_Factura_Compra, FolioFactCompraDetalleCeldaSeleccionada, CodigoProdFactCompraDetalleCeldaSeleccionada,
                        CantidadFactCompraDetalleCeldaSeleccionada, PrecioFactCompraDetalleCeldaSeleccionada)
                    {
                        Folio = FolioFactCompraDetalleCeldaSeleccionada,
                        Codigo_Producto = CodigoProdFactCompraDetalleCeldaSeleccionada,
                        Cantidad = CantidadFactCompraDetalleCeldaSeleccionada,
                        Precio = PrecioFactCompraDetalleCeldaSeleccionada
                    };

                    await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Factura_Compra_Detalle", OldFacturaCompraDetalle);
                }
                catch (Exception Ex)
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                    Console.WriteLine("Error Editar FacturaCompraDetalleViewModel: " + Ex.Message);
                }
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Debe seleccionar una fila valida", "Ok");
            }


        }

        private async Task CargarFactCompraDetalles()
        {
            try
            {
                var factCompraDetalles = await _factComprDetalleService.GetFactCompraDetalleFilterByIdAsync(Id_Factura_Compra);
                FacturaCompraDetalleModels.Clear();
                foreach (var factCompraDetalle in factCompraDetalles)
                {
                    FacturaCompraDetalleModels.Add(factCompraDetalle);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error CargarFactVentaDetalle InsertarFactVentaDetalle: " + Ex.Message);
            }
        }

        private async Task AgregarFactCompraDetalle(FacturaCompraDetalleModel facturaCompraDetalleModel)
        {
            try
            {
                if (!(PrecioFactCompraDetalleIngresado > MAX_INT))
                {
                    if (await _factComprDetalleService.AddFactCompraDetalleAsync(facturaCompraDetalleModel))
                    {
                        FacturaCompraDetalleModels.Add(facturaCompraDetalleModel);
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
                Console.WriteLine("Error AgregarFactCompraDetalle FacturaCompraDetalleViewModel: " + Ex.Message);
            }
        }

        private async Task EliminarFactCompraDetalle(int id)
        {
            try
            {
                if (await _factComprDetalleService.DeleteFactCompraDetalleAsync(id))
                {
                    var producto = FacturaCompraDetalleModels.FirstOrDefault(p => p.Id_Factura_Compra_Detalle == id);
                    if (producto != null)
                    {
                        FacturaCompraDetalleModels.Remove(producto);
                    }
                }
            }
            catch (Exception Ex) {
                Console.WriteLine("Error EliminarFactCompraDetalle FacturaCompraDetalleViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public void Update()
        {
            try
            {
                ActualizarFactCompraDetalle((FacturaCompraDetalleModel)OldFacturaCompraDetalle);
                _navigationService.GoBackAsync();
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error Update FacturaCompraDetalleViewModel: " + Ex.Message);
            }
        }

        private async Task ActualizarFactCompraDetalle(FacturaCompraDetalleModel AntiguaFactCompraDetalle)
        {
            try
            {
                Console.WriteLine("EditFolioFactCompraDetalle: " + EditFolioFactCompraDetalle);
                Console.WriteLine("EditCodigoProdFactCompraDetalle: " + EditCodigoProdFactCompraDetalle);
                Console.WriteLine("EditCantidadFactCompraDetalle: " + EditCantidadFactCompraDetalle);
                Console.WriteLine("EditPrecioFactCompraDetalle: " + EditPrecioFactCompraDetalle);

                var id = IdFactCompraDetalleCeldaSeleccionada;
                var idFacturaCompra = Id_Factura_Compra;
                var folio = EditFolioFactCompraDetalle;
                var codigo = EditCodigoProdFactCompraDetalle;
                var cantidad = CantidadFactCompraDetalleIngresado;
                var precio = (int)PrecioFactCompraDetalleIngresado;

                NewFacturaCompraDetalle = new FacturaCompraDetalleModel(id, idFacturaCompra, folio, codigo, cantidad, precio)
                {
                    Id_Factura_Compra_Detalle = id,
                    Id_Factura_Compra = idFacturaCompra,
                    Folio = folio,
                    Codigo_Producto = codigo,
                    Cantidad = cantidad,
                    Precio = precio
                };

                if (!(EditPrecioFactCompraDetalle > MAX_INT))
                {
                    if (await _factComprDetalleService.UpdateFactCompraDetalleAsync((FacturaCompraDetalleModel)NewFacturaCompraDetalle))
                    {
                        //Remove Old Product
                        FacturaCompraDetalleModels.Remove(AntiguaFactCompraDetalle);
                        //Add new product
                        FacturaCompraDetalleModels.Add((FacturaCompraDetalleModel)NewFacturaCompraDetalle);
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
                Console.WriteLine("Error ActualizarFactCompraDetalle FacturaCompraDetalleViewModel: " + Ex.Message);
            }
        }
        #endregion
    }
}
