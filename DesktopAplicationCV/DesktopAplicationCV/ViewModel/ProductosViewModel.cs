using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DesktopAplicationCV.ViewModel
{
    public partial class ProductosViewModel : BaseViewModel
    {
        #region Variables
        private readonly INavigationService _navigationService;

        //public ICommand NavigateToDetailCommand => new AsyncRelayCommand(NavigateToDetail);

        [ObservableProperty]
        private int selectedIndex;

        [ObservableProperty]
        private ObservableCollection<ProductosModel> producto;

        private string _filterText;

        #endregion

        public ObservableCollection<ProductosModel> Items { get; set; }

        #region Inicializadores
        public ObservableCollection<ProductosModel> ProdInfoCollection
        {
            get { return producto; }
            set { producto = value; }
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

        // Acción para establecer la lógica del filtro
        public Action ApplyFilterAction { get; set; }

        #region Constructores
        public ProductosViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            producto = new ObservableCollection<ProductosModel>();
            GenerateOrders();
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

        public void GenerateOrders()
        {
            producto.Add(new ProductosModel(0, "Germany"));
            producto.Add(new ProductosModel(1, "Mexico"));
            producto.Add(new ProductosModel(2, "Mexico"));
            producto.Add(new ProductosModel(3, "UK"));
            producto.Add(new ProductosModel(4, "Sweden"));
            producto.Add(new ProductosModel(5, "Germany"));
            producto.Add(new ProductosModel(6, "France"));
            producto.Add(new ProductosModel(7, "Spain"));
            producto.Add(new ProductosModel(8, "France"));
            producto.Add(new ProductosModel(9, "Canada"));
            producto.Add(new ProductosModel(10, "UK"));
            producto.Add(new ProductosModel(11, "Germany"));
            producto.Add(new ProductosModel(12, "France"));
            producto.Add(new ProductosModel(13, "UK"));
            producto.Add(new ProductosModel(14, "CL"));
            producto.Add(new ProductosModel(15, "CL"));
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    Producto.RemoveAt((SelectedIndex - 1));
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

        //private async Task NavigateToDetail()
        [RelayCommand]
        private async void Editar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Productos");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
            }
        }
    }
}
