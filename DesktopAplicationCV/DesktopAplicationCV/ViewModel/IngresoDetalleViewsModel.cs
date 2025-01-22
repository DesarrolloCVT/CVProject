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
    public partial class IngresoDetalleViewsModel : BaseViewModel
    {
        #region Variables
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private int selectedIndex;

        [ObservableProperty]
        private ObservableCollection<IngresoDetalleModel> detalle;

        private string _filterText;

        #endregion

        public ObservableCollection<IngresoDetalleModel> Items { get; set; }


        #region Inicializadores
        public ObservableCollection<IngresoDetalleModel> IngresoDetalleInfoCollection
        {
            get { return detalle; }
            set { detalle = value; }
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

        public IngresoDetalleViewsModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            detalle = new ObservableCollection<IngresoDetalleModel>();
            GenerateOrders();
        }

        #endregion

        // Lógica de filtrado como delegado
        public Predicate<object> GetFilter()
        {
            return item =>
            {
                if (item is IngresoDetalleModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Folio_Factura_Ventas.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Monto.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        public void GenerateOrders()
        {
            detalle.Add(new IngresoDetalleModel(0, 10500));
            detalle.Add(new IngresoDetalleModel(1, 10500));
            detalle.Add(new IngresoDetalleModel(2, 5500));
            detalle.Add(new IngresoDetalleModel(3, 105880));
            detalle.Add(new IngresoDetalleModel(4, 685399));
            detalle.Add(new IngresoDetalleModel(5, 25108));
            detalle.Add(new IngresoDetalleModel(6, 4578965));
            detalle.Add(new IngresoDetalleModel(7, 18756));
            detalle.Add(new IngresoDetalleModel(8, 659785));
            detalle.Add(new IngresoDetalleModel(9, 1485890));
            detalle.Add(new IngresoDetalleModel(10, 2585));
            detalle.Add(new IngresoDetalleModel(11, 1515454));
            detalle.Add(new IngresoDetalleModel(12, 211868));
            detalle.Add(new IngresoDetalleModel(13, 15850));
            detalle.Add(new IngresoDetalleModel(14, 39850));
            detalle.Add(new IngresoDetalleModel(15, 80000));
        }

        #region Binding Methods 

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    Detalle.RemoveAt((SelectedIndex - 1));
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
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Ingresos_Detalle");
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
                await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Ingresos_Detalle");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
            }
        }

        #endregion
    }
}
