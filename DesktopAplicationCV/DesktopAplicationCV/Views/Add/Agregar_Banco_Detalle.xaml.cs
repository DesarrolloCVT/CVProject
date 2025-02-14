using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Banco_Detalle : ContentPage
{
	public Agregar_Banco_Detalle()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new BancoDetalleViewModel(navigationService);
        var viewModel = BindingContext as BancoDetalleViewModel;
    }
}