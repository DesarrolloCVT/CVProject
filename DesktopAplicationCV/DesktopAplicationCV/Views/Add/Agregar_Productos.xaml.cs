using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Productos : ContentPage
{
	public Agregar_Productos()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new ProductosViewModel(navigationService);
        var viewModel = BindingContext as ProductosViewModel;
    }
}