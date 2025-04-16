using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using Syncfusion.Maui.DataGrid;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DesktopAplicationCV.ViewModel
{
    public partial class ProductosViewModel : BaseViewModel
    {
        #region Variables

        private static int IdProductoSeleccionado;

        private static object _oldProduct;
        private object NewProduct;

        private string CodigoCeldaSeleccionada;
        private string NombreProductoCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly ProductoService _productoService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<ProductosModel> Productos;

        private string _filterText;
        private string _nombreProductoIngresadoText;
        private string _codigoProductoIngresadoText;
        
        private string _editNombreProducto;
        private string _editCodigoProducto;

        public ICommand CargarProductosCommand { get; }
        public ICommand AgregarProductoCommand { get; }
        public ICommand EliminarProductoCommand { get; }
        public ICommand ActualizarProductoCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Encapsulado

        public ObservableCollection<ProductosModel> ProdInfoCollection
        {
            get { return Productos; }
            set { Productos = value; }
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

        public object OldProduct
        {
            get => _oldProduct;
            set
            {
                if (_oldProduct != value)
                {
                    _oldProduct = value;
                }
            }
        }

        public string NombreProductoIngresado
        {
            get => _nombreProductoIngresadoText;
            set
            {
                if (_nombreProductoIngresadoText != value)
                {
                    _nombreProductoIngresadoText = value;
                    OnPropertyChanged(nameof(NombreProductoIngresado));
                }
            }
        }

        public string EditNombreProducto
        {
            get => _editNombreProducto;
            set
            {
                if (_editNombreProducto != value)
                {
                    _editNombreProducto = value;
                    OnPropertyChanged(nameof(EditNombreProducto));
                }
            }
        }

        public string CodigoProductoIngresado
        {
            get => _codigoProductoIngresadoText;
            set
            {
                if (_codigoProductoIngresadoText != value)
                {
                    _codigoProductoIngresadoText = value;
                    OnPropertyChanged(nameof(CodigoProductoIngresado));
                }
            }
        }

        public string EditCodigoProducto
        {
            get => _editCodigoProducto;
            set
            {
                if (_editCodigoProducto != value)
                {
                    _editCodigoProducto = value;
                    OnPropertyChanged(nameof(EditCodigoProducto));
                }
            }
        }

        #endregion

        #region Constructores

        public ProductosViewModel(INavigationService navigationService)
        {
            _productoService = new ProductoService();
            Productos = new ObservableCollection<ProductosModel>();

            _navigationService = navigationService;
            CargarProductos();

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
                if (item is ProductosModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Codigo.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Producto.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        // Método que se ejecuta cuando se toca una celda
        private void CeldaTocada(DataGridCellTappedEventArgs e)
        {
            try
            {
                if (e.RowData is ProductosModel productos)
                {
                    IdProductoSeleccionado = productos.Id_Producto;
                    CodigoCeldaSeleccionada = productos.Codigo;
                    NombreProductoCeldaSeleccionada = productos.Producto;
                    // Aquí puedes manejar la lógica de negocio sin tocar la vista
                }
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error CeldaTocada ProductosViewModel: " + Ex.Message);
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
            CargarProductos();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    EliminarProducto(IdProductoSeleccionado);
                    CargarProductos();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
                Console.WriteLine("Error Eliminar ProductosViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void Agregar()
        {   
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Productos");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                Console.WriteLine("Error Agregar ProductosViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void InsertarProducto()
        {
            try
            {
                if (!string.IsNullOrEmpty(CodigoProductoIngresado) && !string.IsNullOrEmpty(NombreProductoIngresado))
                {
                    AgregarProducto(new ProductosModel(IdProductoSeleccionado, CodigoProductoIngresado, NombreProductoIngresado));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine("Error InsertarProducto ProductosViewModel: " + Ex.Message);
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
                        OldProduct = new ProductosModel(IdProductoSeleccionado, CodigoCeldaSeleccionada, NombreProductoCeldaSeleccionada)
                        {
                            Id_Producto = IdProductoSeleccionado,
                            Codigo = CodigoCeldaSeleccionada,
                            Producto = NombreProductoCeldaSeleccionada
                        };

                        await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Productos", OldProduct);
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
                Console.WriteLine("Error Editar ProductosViewModel: " + Ex.Message);
            }
        }

        private async Task CargarProductos()
        {
            try
            {
                var productos = await _productoService.GetProductosAsync();
                Productos.Clear();
                foreach (var producto in productos)
                {
                    Productos.Add(producto);
                }
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error CargarProductos ProductosViewModel: " + Ex.Message);
            }
        }

        private async Task AgregarProducto(ProductosModel producto)
        {
            try
            {
                if (await _productoService.AddProductoAsync(producto))
                {
                    Productos.Add(producto);
                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente. ", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error, ya existe una entrada asignada con este Codigo.", "Ok");
                }
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error AgregarProducto ProductosViewModel: " + Ex.Message);
            }
        }

        private async Task EliminarProducto(int id)
        {
            try
            {
                if (await _productoService.DeleteProductoAsync(id))
                {
                    var producto = Productos.FirstOrDefault(p => p.Id_Producto == id);
                    if (producto != null)
                    {
                        Productos.Remove(producto);
                    }
                }
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error EliminarProducto ProductosViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public void Update()
        {
            try
            {
                ActualizarProducto((ProductosModel)OldProduct);
                _navigationService.GoBackAsync();
            }
            catch (Exception Ex) {
                Console.WriteLine("Error Update ProductosViewModel: " + Ex.Message);
            }
            
        }

        private async Task ActualizarProducto(ProductosModel AntiguoProducto)
        {
            try
            {
                Console.WriteLine("EditCodigoProducto: " + EditCodigoProducto);
                Console.WriteLine("EditNombreProducto: " + EditNombreProducto);

                var id = IdProductoSeleccionado;
                var cod = EditCodigoProducto;
                var prod = EditNombreProducto;

                NewProduct = new ProductosModel(id, cod, prod)
                {
                    Id_Producto = id,
                    Codigo = cod,
                    Producto = prod
                };

                if (await _productoService.UpdateProductoAsync((ProductosModel)NewProduct))
                {
                    //Remove Old Product
                    Productos.Remove(AntiguoProducto);

                    //Add new product
                    Productos.Add((ProductosModel)NewProduct);

                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error al editar", "Ok");
                }
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error ActualizarBancoDetalle ProductosViewModel: " + Ex.Message);
            }
        }
        #endregion
    }
}
