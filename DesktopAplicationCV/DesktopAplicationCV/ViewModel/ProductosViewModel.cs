using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Views.Add;

namespace DesktopAplicationCV.ViewModel
{
    public partial class ProductosViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string codigo = "Codigo";

        [ObservableProperty]
        public string producto = "Producto";

        public ProductosViewModel(INavigationService navigationService)
        {

        }

        public ProductosViewModel()
        {

        }

        [RelayCommand]
        public void BtnAgregar()
        {
            Application.Current.MainPage.DisplayAlert("Alerta", "Comando agregar", "Ok");
        }

        [RelayCommand]
        public void BtnEliminar()
        {
            Application.Current.MainPage.DisplayAlert("Alerta", "Comando eliminar", "Ok");
        }

        [RelayCommand]
        public void BtnEditar()
        {
            Application.Current.MainPage.DisplayAlert("Alerta", "Comando editar", "Ok");
        }
    }
}
