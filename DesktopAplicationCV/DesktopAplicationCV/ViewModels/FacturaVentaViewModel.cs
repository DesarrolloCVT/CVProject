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
    public partial class FacturaVentaViewModel : BaseViewModel
    {
        #region Variables
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private int selectedIndex;

        [ObservableProperty]
        private ObservableCollection<FacturaVentaModel> factura;

        private string _filterText;

        #endregion

        public ObservableCollection<FacturaVentaModel> Items { get; set; }


        #region Inicializadores
        public ObservableCollection<FacturaVentaModel> FacturaVentaInfoCollection
        {
            get { return factura; }
            set { factura = value; }
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

        public FacturaVentaViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            factura = new ObservableCollection<FacturaVentaModel>();
            GenerateOrders();
        }

        #endregion

        // Lógica de filtrado como delegado
        public Predicate<object> GetFilter()
        {
            return item =>
            {
                if (item is FacturaVentaModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Folio.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Cliente.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Direccion_Despacho.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Moneda.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Fecha.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        public void GenerateOrders()
        {
            factura.Add(new FacturaVentaModel(0, "Germany", "ALFKI", 10, "10/01/254"));
            factura.Add(new FacturaVentaModel(1, "Mexico", "ANATR", 10, "10/01/254"));
            factura.Add(new FacturaVentaModel(2, "Mexico", "ANTON", 10, "10/01/254"));
            factura.Add(new FacturaVentaModel(3, "UK", "AROUT", 10, "10/01/254"));
            factura.Add(new FacturaVentaModel(4, "Sweden", "BERGS", 10, "10/01/254"));
            factura.Add(new FacturaVentaModel(5, "Germany", "BLAUS", 10, "10/01/254"));
            factura.Add(new FacturaVentaModel(6, "France", "BLONP", 10, "10/01/254"));
            factura.Add(new FacturaVentaModel(7, "Spain", "BOLID", 10, "10/01/254"));
            factura.Add(new FacturaVentaModel(8, "France", "BONAP", 10, "10/01/254"));
            factura.Add(new FacturaVentaModel(9, "Canada", "BOTTM", 10, "10/01/254"));
            factura.Add(new FacturaVentaModel(10, "UK", "AROUT", 10, "10/01/254"));
            factura.Add(new FacturaVentaModel(11, "Germany", "BLAUS", 10, "10/01/254"));
            factura.Add(new FacturaVentaModel(12, "France", "BLONP", 10, "10/01/254"));
            factura.Add(new FacturaVentaModel(13, "UK", "AROUT", 10, "10/01/254"));
            factura.Add(new FacturaVentaModel(14, "CL", "TANGANANA", 1050, "10/01/254"));
            factura.Add(new FacturaVentaModel(15, "CL", "TANGANANICA", 3550, "10/01/254"));
        }

        #region Binding Methods 

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    factura.RemoveAt((SelectedIndex - 1));
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
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Factura_Ventas");
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
                await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Factura_Ventas");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
            }
        }

        #endregion
    }
}
