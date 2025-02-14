using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Banco : ContentPage
{
	public Agregar_Banco()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new BancoViewModel(navigationService);
        var viewModel = BindingContext as BancoViewModel;
    }
}