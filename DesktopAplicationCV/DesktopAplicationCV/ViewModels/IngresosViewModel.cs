using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.ViewModel
{
    public partial class IngresosViewModel : BaseViewModel
    {
        #region Variables
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private int selectedIndex;

        [ObservableProperty]
        private ObservableCollection<IngresosModel> ingresos;

        private string _filterText;

        #endregion

        public ObservableCollection<IngresosModel> Items { get; set; }


        #region Inicializadores
        public ObservableCollection<IngresosModel> IngresosInfoCollection
        {
            get { return ingresos; }
            set { ingresos = value; }
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

        public IngresosViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ingresos = new ObservableCollection<IngresosModel>();
            GenerateOrders();
        }

        #endregion

        // Lógica de filtrado como delegado
        public Predicate<object> GetFilter()
        {
            return item =>
            {
                if (item is IngresosModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Folio.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Tipo.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Moneda.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Fecha.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Cliente.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Metodo_Pago.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Banco.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Cuenta.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        public void GenerateOrders()
        {
            ingresos.Add(new IngresosModel(0, "Cliente", "ALFKI", "01/02/2025", "Cliente Nuevo", "Efectivo", "Itau", 10));
            ingresos.Add(new IngresosModel(1, "Proveedor", "ALFKI", "01/02/2025", "Cliente Antiguo", "Tarjeta", "Estado", 10));
            ingresos.Add(new IngresosModel(2, "Proveedor", "ALFKI", "01/02/2025", "Cliente Nuevo", "Efectivo", "Itau", 10));
            ingresos.Add(new IngresosModel(3, "Cliente", "ALFKI", "01/02/2025", "Cliente Antiguo", "Tarjeta", "Estado", 10));
            ingresos.Add(new IngresosModel(4, "Proveedor", "ALFKI", "01/02/2025", "Cliente Nuevo", "Efectivo", "Itau", 10));
            ingresos.Add(new IngresosModel(5, "Cliente", "ALFKI", "01/02/2025", "Cliente Antiguo", "Tarjeta", "Bice", 10));
        }

        #region Binding Methods 

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    Ingresos.RemoveAt((SelectedIndex - 1));
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
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Ingresos");
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
                await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Ingresos");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
            }
        }

        #endregion
    }
}
