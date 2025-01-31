using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.Views;
using System.Collections.ObjectModel;

namespace DesktopAplicationCV.ViewModel
{
    public partial class TipoViewModel : BaseViewModel
    {
        #region Variables
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private int selectedIndex;

        [ObservableProperty]
        private ObservableCollection<TipoModel> tipo;

        private string _filterText;

        #endregion

        public ObservableCollection<TipoModel> Items { get; set; }


        #region Inicializadores
        public ObservableCollection<TipoModel> TipoInfoCollection
        {
            get { return tipo; }
            set { tipo = value; }
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

        public TipoViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            tipo = new ObservableCollection<TipoModel>();
            GenerateOrders();
        }

        #endregion

        // Lógica de filtrado como delegado
        public Predicate<object> GetFilter()
        {
            return item =>
            {
                if (item is TipoModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Nombre.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Codigo.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Tipo.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Cuenta.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        public void GenerateOrders()
        {
            tipo.Add(new TipoModel(0, "Germany", "ALFKI", 10));
            tipo.Add(new TipoModel(1, "Mexico", "ANATR", 10));
            tipo.Add(new TipoModel(2, "Mexico", "ANTON", 10));
            tipo.Add(new TipoModel(3, "UK", "AROUT", 10));
            tipo.Add(new TipoModel(4, "Sweden", "BERGS", 10));
            tipo.Add(new TipoModel(5, "Germany", "BLAUS", 10));
            tipo.Add(new TipoModel(6, "France", "BLONP", 10));
            tipo.Add(new TipoModel(7, "Spain", "BOLID", 10));
            tipo.Add(new TipoModel(8, "France", "BONAP", 10));
            tipo.Add(new TipoModel(9, "Canada", "BOTTM", 10));
            tipo.Add(new TipoModel(10, "UK", "AROUT", 10));
            tipo.Add(new TipoModel(11, "Germany", "BLAUS", 10));
            tipo.Add(new TipoModel(12, "France", "BLONP", 10));
            tipo.Add(new TipoModel(13, "UK", "AROUT", 10));
            tipo.Add(new TipoModel(14, "CL", "TANGANANA", 1050));
            tipo.Add(new TipoModel(15, "CL", "TANGANANICA", 3550));
        }

        #region Binding Methods 

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    Tipo.RemoveAt((SelectedIndex - 1));
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
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Tipo");
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
                await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Tipo");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
            }
        }

        #endregion
    }
}
