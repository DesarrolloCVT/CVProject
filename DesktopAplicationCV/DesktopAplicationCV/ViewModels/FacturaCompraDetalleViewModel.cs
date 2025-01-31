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
    public partial class FacturaCompraDetalleViewModel : BaseViewModel
    {
        #region Variables
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private int selectedIndex;

        [ObservableProperty]
        private ObservableCollection<FacturaCompraDetalleModel> compraDetalle;

        private string _filterText;

        #endregion

        public ObservableCollection<FacturaCompraDetalleModel> Items { get; set; }


        #region Inicializadores
        public ObservableCollection<FacturaCompraDetalleModel> FacturaCompraDetalleInfoCollection
        {
            get { return compraDetalle; }
            set { compraDetalle = value; }
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

        public FacturaCompraDetalleViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            compraDetalle = new ObservableCollection<FacturaCompraDetalleModel>();
            GenerateOrders();
        }

        #endregion

        // Lógica de filtrado como delegado
        public Predicate<object> GetFilter()
        {
            return item =>
            {
                if (item is FacturaCompraDetalleModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Codigo_Producto.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Fecha.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Moneda.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        public void GenerateOrders()
        {
            compraDetalle.Add(new FacturaCompraDetalleModel(0, "01/02/25",10));
            compraDetalle.Add(new FacturaCompraDetalleModel(1, "01/02/25", 10));
            compraDetalle.Add(new FacturaCompraDetalleModel(2, "01/02/25", 10));
            compraDetalle.Add(new FacturaCompraDetalleModel(3, "01/02/25", 10));
            compraDetalle.Add(new FacturaCompraDetalleModel(4, "01/02/25", 10));
            compraDetalle.Add(new FacturaCompraDetalleModel(5, "01/02/25", 10));
            compraDetalle.Add(new FacturaCompraDetalleModel(6, "01/02/25", 10));
            compraDetalle.Add(new FacturaCompraDetalleModel(7, "01/02/25", 10));
            compraDetalle.Add(new FacturaCompraDetalleModel(8, "01/02/25", 10));
            compraDetalle.Add(new FacturaCompraDetalleModel(9, "01/02/25", 10));
            compraDetalle.Add(new FacturaCompraDetalleModel(10, "01/02/25", 10));
            compraDetalle.Add(new FacturaCompraDetalleModel(11, "01/02/25", 10));
            compraDetalle.Add(new FacturaCompraDetalleModel(12, "01/02/25", 10));
            compraDetalle.Add(new FacturaCompraDetalleModel(13, "01/02/25", 10));
            compraDetalle.Add(new FacturaCompraDetalleModel(14, "01/02/25", 10));
            compraDetalle.Add(new FacturaCompraDetalleModel(15, "01/02/25", 10));
        }

        #region Binding Methods 

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    CompraDetalle.RemoveAt((SelectedIndex - 1));
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
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Factura_Compras_Detalle");
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
                await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Factura_Compra_Detalle");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
            }
        }

        #endregion
    }
}
