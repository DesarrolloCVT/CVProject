﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DesktopAplicationCV.ViewModel
{
    public partial class SocioViewModel : BaseViewModel
    {
        #region Variables
        private readonly INavigationService _navigationService;
        
        [ObservableProperty]
        private int selectedIndex;

        [ObservableProperty]
        private ObservableCollection<SocioNegocio> socio;

        private string _filterText;

        #endregion

        public ObservableCollection<SocioNegocio> Items { get; set; }

        
        #region Inicializadores
        public ObservableCollection<SocioNegocio> SocioInfoCollection
        {
            get { return socio; }
            set { socio = value; }
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

        public SocioViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            socio = new ObservableCollection<SocioNegocio>();
            GenerateOrders();
        }

        #endregion

        // Lógica de filtrado como delegado
        public Predicate<object> GetFilter()
        {
            return item =>
            {
                if (item is SocioNegocio data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Nombre.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Codigo.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Tipo.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Saldo.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        public void GenerateOrders()
        {
            socio.Add(new SocioNegocio(0,"Germany", "ALFKI", 10));
            socio.Add(new SocioNegocio(1,"Mexico", "ANATR", 10));
            socio.Add(new SocioNegocio(2,"Mexico", "ANTON", 10));
            socio.Add(new SocioNegocio(3,"UK", "AROUT", 10));
            socio.Add(new SocioNegocio(4,"Sweden", "BERGS", 10));
            socio.Add(new SocioNegocio(5,"Germany", "BLAUS", 10));
            socio.Add(new SocioNegocio(6,"France", "BLONP", 10));
            socio.Add(new SocioNegocio(7,"Spain", "BOLID", 10));
            socio.Add(new SocioNegocio(8,"France", "BONAP", 10));
            socio.Add(new SocioNegocio(9,"Canada", "BOTTM", 10));
            socio.Add(new SocioNegocio(10,"UK", "AROUT", 10));
            socio.Add(new SocioNegocio(11,"Germany", "BLAUS", 10));
            socio.Add(new SocioNegocio(12,"France", "BLONP", 10));
            socio.Add(new SocioNegocio(13,"UK", "AROUT", 10));
            socio.Add(new SocioNegocio(14,"CL", "TANGANANA", 1050));
            socio.Add(new SocioNegocio(15,"CL", "TANGANANICA", 3550));
        }

        #region Binding Methods 

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if(selectedIndex >= 0)
                {
                    Socio.RemoveAt((SelectedIndex - 1));
                }
                else {
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
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Socio_Negocio");
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
                await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Socio_Negocio");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
            }
        }

        #endregion
    }
}
