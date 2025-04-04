using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Metodo_Pago : ContentPage
{
	public Agregar_Metodo_Pago()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new MetodoPagoViewModel(navigationService);
        var viewModel = BindingContext as MetodoPagoViewModel;
    }
}