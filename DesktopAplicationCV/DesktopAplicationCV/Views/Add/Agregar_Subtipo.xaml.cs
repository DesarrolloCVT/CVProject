using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Subtipo : ContentPage
{
	public Agregar_Subtipo()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new SubtiposViewModel(navigationService);
        var viewModel = BindingContext as SubtiposViewModel;
    }
}