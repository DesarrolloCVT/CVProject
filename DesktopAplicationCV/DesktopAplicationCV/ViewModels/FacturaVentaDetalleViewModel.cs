using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.ViewModel
{
    public partial class FacturaVentaDetalleViewModel : BaseViewModel
    {
        #region Variables
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private int selectedIndex;

        [ObservableProperty]
        private ObservableCollection<FacturaVentaDetalleModel> facturaDetalle;

        private string _filterText;

        #endregion

        public ObservableCollection<FacturaVentaDetalleModel> Items { get; set; }


        #region Inicializadores
        public ObservableCollection<FacturaVentaDetalleModel> FacturaVentaDetalleInfoCollection
        {
            get { return facturaDetalle; }
            set { facturaDetalle = value; }
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

        public FacturaVentaDetalleViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            facturaDetalle = new ObservableCollection<FacturaVentaDetalleModel>();
            GenerateOrders();
        }

        #endregion

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

        public void GenerateOrders()
        {
            facturaDetalle.Add(new FacturaVentaDetalleModel(0, 2506, 5000, 20000100));
            facturaDetalle.Add(new FacturaVentaDetalleModel(1, 2506, 5000, 20000100));
            facturaDetalle.Add(new FacturaVentaDetalleModel(2, 2506, 5000, 20000100));
            facturaDetalle.Add(new FacturaVentaDetalleModel(3, 2506, 5000, 20000100));
            facturaDetalle.Add(new FacturaVentaDetalleModel(4, 2506, 5000, 20000100));
            facturaDetalle.Add(new FacturaVentaDetalleModel(5, 2506, 5000, 20000100));
            facturaDetalle.Add(new FacturaVentaDetalleModel(6, 2506, 5000, 20000100));
            facturaDetalle.Add(new FacturaVentaDetalleModel(7, 2506, 5000, 20000100));
            facturaDetalle.Add(new FacturaVentaDetalleModel(8, 2506, 5000, 20000100));
        }

        #region Binding Methods 

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    facturaDetalle.RemoveAt((SelectedIndex - 1));
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
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Factura_Venta_Detalle");
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
                await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Factura_Venta_Detalle");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
            }
        }

        #endregion
    }
}
