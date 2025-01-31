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

        private object OldProduct;
        private object NewProduct;

        private int CodigoCeldaSeleccionada;
        private string NombreProductoCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly ProductoService _productoService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<ProductosModel> Productos;

        private string _filterText;
        private string _nombreProductoIngresadoText;
        private int _codigoProductoIngresadoText;
        private string _editNombreProducto;
        private int _editCodigoProducto;

        public ICommand CargarProductosCommand { get; }
        public ICommand AgregarProductoCommand { get; }
        public ICommand EliminarProductoCommand { get; }
        public ICommand ActualizarProductoCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Inicializadores
        public ObservableCollection<ProductosModel> ProdInfoCollection
        {
            get { return Productos; }
            set { Productos = value; }
        }
        #endregion

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

        public int CodigoProductoIngresado
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

        public int EditCodigoProducto
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


        // Acción para establecer la lógica del filtro
        public Action ApplyFilterAction { get; set; }

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
            if (e.RowData is ProductosModel productos)
            {
                CodigoCeldaSeleccionada = productos.Codigo;
                NombreProductoCeldaSeleccionada = productos.Producto;
                // Aquí puedes manejar la lógica de negocio sin tocar la vista
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
                    EliminarProducto(CodigoCeldaSeleccionada);
                    CargarProductos();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
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
            }
        }

        [RelayCommand]
        public async void InsertarProducto()
        {
            if(CodigoProductoIngresado != 0 && !string.IsNullOrEmpty(NombreProductoIngresado))
            {
                AgregarProducto(new ProductosModel(CodigoProductoIngresado, NombreProductoIngresado));
                Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente", "Ok");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
            }
        }

        [RelayCommand]
        private async void Editar()
        {
            if (selectedIndex >= 0)
            {
                try
                {
                    OldProduct = new ProductosModel(CodigoCeldaSeleccionada, NombreProductoCeldaSeleccionada)
                    {
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

        private async Task CargarProductos()
        {
            var productos = await _productoService.GetProductosAsync();
            Productos.Clear();
            foreach (var producto in productos)
            {
                Productos.Add(producto);
            }
        }

        private async Task AgregarProducto(ProductosModel producto)
        {
            if (await _productoService.AddProductoAsync(producto))
            {
                Productos.Add(producto);
            }
        }

        private async Task EliminarProducto(int codigo)
        {
            if (await _productoService.DeleteProductoAsync(codigo))
            {
                var producto = Productos.FirstOrDefault(p => p.Codigo == codigo);
                if (producto != null)
                {
                    Productos.Remove(producto);
                }
            }
        }

        [RelayCommand]
        public void Update()
        {
            ActualizarProducto((ProductosModel)OldProduct);
        }


        private async Task ActualizarProducto(ProductosModel AntiguoProducto)
        {
            var cod = 1;
            var prod = "Porotos";
            NewProduct = new ProductosModel(cod, prod)
            {
                Codigo = cod,
                Producto = prod
            };

            if (await _productoService.UpdateProductoAsync((ProductosModel)NewProduct))
            {
                //Remove Old Product
                Productos.Remove(AntiguoProducto);

                //Add new product
                Productos.Add((ProductosModel)NewProduct);

            }
        }
    }
}
