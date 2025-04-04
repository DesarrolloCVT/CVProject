using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Monedas : ContentPage
{
	public Agregar_Monedas()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new MonedasViewModel(navigationService);
        var viewModel = BindingContext as MonedasViewModel;
    }
}