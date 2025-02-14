using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Cuentas : ContentPage
{
	public Agregar_Cuentas()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new CuentasViewModel(navigationService);
        var viewModel = BindingContext as CuentasViewModel;
    }
}