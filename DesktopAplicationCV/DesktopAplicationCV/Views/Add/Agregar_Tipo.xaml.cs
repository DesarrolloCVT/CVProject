using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.ViewModels;

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

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is TipoViewModel vm)
            await vm.CargarTipos();
    }
}