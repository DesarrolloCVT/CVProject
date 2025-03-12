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
    public partial class FacturaVentaDetalleViewModel : BaseViewModel
    {
        #region Variables

        private static object _oldFactVentaDetalle;
        private object NewFactVentaDetalle;

        private int FolioFactVentaDetalleCeldaSeleccionada;
        private string CodProductoFactVentaDetalleCeldaSeleccionada;
        private int CantidadFactVentaDetalleCeldaSeleccionada;
        private int PrecioFactVentaDetalleCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly FacturaVentaDetalleService _factVentaDetalleService;

        [ObservableProperty]
        private int selectedIndex;

        [ObservableProperty]
        private ObservableCollection<FacturaVentaDetalleModel> facturaVentaDetalleModels;

        private string _filterText;

        private int _folioFactVentaDetalleIngresado;
        private string _codProductoFactVentaDetalleIngresado;
        private int _cantidadFactVentaDetalleIngresado;
        private int _precioFactVentaDetalleIngresado;

        private int _editfolioFactVentaDetalle;
        private string _editCodProductoFactVentaDetalle;
        private int _editCantidadFactVentaDetalle;
        private int _editPrecioFactVentaDetalle;

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

        public int CantidadFactVentaDetalleIngresado
        {
            get => _cantidadFactVentaDetalleIngresado;
            set
            {
                if (_cantidadFactVentaDetalleIngresado != value)
                {
                    _cantidadFactVentaDetalleIngresado = value;
                    OnPropertyChanged(nameof(CantidadFactVentaDetalleIngresado));
                }
            }
        }

        public int PrecioFactVentaDetalleIngresado
        {
            get => _precioFactVentaDetalleIngresado;
            set
            {
                if (_precioFactVentaDetalleIngresado != value)
                {
                    _precioFactVentaDetalleIngresado = value;
                    OnPropertyChanged(nameof(PrecioFactVentaDetalleIngresado));
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

        public int EditPrecioFactVentaDetalle
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

        #endregion

        #region Constructores

        public FacturaVentaDetalleViewModel(INavigationService navigationService)
        {
            _factVentaDetalleService = new FacturaVentaDetalleService();
            FacturaVentaDetalleInfoCollection = new ObservableCollection<FacturaVentaDetalleModel>();

            _navigationService = navigationService;
            CargarFactVentaDetalle();

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
                    EliminarFactVentaDetalle(FolioFactVentaDetalleCeldaSeleccionada);
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
                if (FolioFactVentaDetalleIngresado != 0 && !string.IsNullOrEmpty(CodProductoFactVentaDetalleIngresado)
                && CantidadFactVentaDetalleIngresado != 0 && PrecioFactVentaDetalleIngresado != 0)
                {
                    AgregarFactVentaDetalle(new FacturaVentaDetalleModel(FolioFactVentaDetalleIngresado, CodProductoFactVentaDetalleIngresado,
                        CantidadFactVentaDetalleIngresado, PrecioFactVentaDetalleIngresado));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
                }

            }
            catch(Exception Ex)
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
                        OldFactVentaDetalle = new FacturaVentaDetalleModel(FolioFactVentaDetalleCeldaSeleccionada,
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
            catch(Exception Ex)
            {
                Console.WriteLine("Error Editar InsertarFactVentaDetalle: " + Ex.Message);
            }
        }

        private async Task CargarFactVentaDetalle()
        {
            try
            {
                var factVentaDetalles = await _factVentaDetalleService.GetFactVentaDetallesAsync();
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
            catch (Exception Ex) 
            {
                Console.WriteLine("Error AgregarFactVentaDetalle InsertarFactVentaDetalle: " + Ex.Message);
            }
        }

        private async Task EliminarFactVentaDetalle(int folio)
        {
            try
            {
                if (await _factVentaDetalleService.DeleteFactVentaDetalleAsync(folio))
                {
                    var facturaVentaDetalle = facturaVentaDetalleModels.FirstOrDefault(p => p.Folio == folio);
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
                Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");
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

                var folio = EditfolioFactVentaDetalle;
                var codigo = EditCodProductoFactVentaDetalle;
                var cantidad = EditCantidadFactVentaDetalle;
                var precio = EditPrecioFactVentaDetalle;

                NewFactVentaDetalle = new FacturaVentaDetalleModel(folio, codigo, cantidad, precio)
                {
                    Folio = folio,
                    Codigo_Producto = codigo,
                    Cantidad = cantidad,
                    Precio = precio
                };

                if (await _factVentaDetalleService.UpdateFactVentaDetalleAsync((FacturaVentaDetalleModel)NewFactVentaDetalle))
                {
                    //Remove Old Product
                    facturaVentaDetalleModels.Remove(AntiguoFactVentaDetalle);

                    //Add new product
                    facturaVentaDetalleModels.Add((FacturaVentaDetalleModel)NewFactVentaDetalle);

                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error ActualizarFactVentaDetalle InsertarFactVentaDetalle: " + Ex.Message);
            }
        }
    }
}
