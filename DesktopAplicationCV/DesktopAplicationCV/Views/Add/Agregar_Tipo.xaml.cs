using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Tipo : ContentPage
{
	public Agregar_Tipo()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new TipoViewModel(navigationService);
        var viewModel = BindingContext as TipoViewModel;
    }
}