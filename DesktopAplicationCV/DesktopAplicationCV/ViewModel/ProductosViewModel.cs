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

        public ICommand NavigateToDetailCommand => new AsyncRelayCommand(NavigateToDetail);

        [ObservableProperty]
        private int selectedIndex;

        [ObservableProperty]
        private ObservableCollection<Productos> producto;

        private string _filterText;

        #endregion

        #region Constructores
        public ProductosViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            producto = new ObservableCollection<Productos>();
            GenerateOrders();
        }
        #endregion


        public ObservableCollection<Productos> Items { get; set; }

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

        

        // Lógica de filtrado como delegado
        public Predicate<object> GetFilter()
        {
            return item =>
            {
                if (item is Productos data)
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
            producto.Add(new Productos(0, "Germany"));
            producto.Add(new Productos(1, "Mexico"));
            producto.Add(new Productos(2, "Mexico"));
            producto.Add(new Productos(3, "UK"));
            producto.Add(new Productos(4, "Sweden"));
            producto.Add(new Productos(5, "Germany"));
            producto.Add(new Productos(6, "France"));
            producto.Add(new Productos(7, "Spain"));
            producto.Add(new Productos(8, "France"));
            producto.Add(new Productos(9, "Canada"));
            producto.Add(new Productos(10, "UK"));
            producto.Add(new Productos(11, "Germany"));
            producto.Add(new Productos(12, "France"));
            producto.Add(new Productos(13, "UK"));
            producto.Add(new Productos(14, "CL"));
            producto.Add(new Productos(15, "CL"));
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
        public void Agregar()
        {
            Application.Current.MainPage.DisplayAlert("Alerta", "Selected.Rows - Count: ", "OK");
        }

        private async Task NavigateToDetail()
        {
            try
            {
                await _navigationService.NavigateToAsync<EditarViewModel>("_Socio_Negocio");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
            }
        }
    }
}
