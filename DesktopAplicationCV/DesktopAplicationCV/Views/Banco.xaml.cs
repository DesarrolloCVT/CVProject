using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Banco : ContentPage
{
	public Banco()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new BancoViewModel(navigationService);

        var viewModel = BindingContext as BancoViewModel;

        if (viewModel != null)
        {
            // Vincular la acci�n para aplicar el filtro
            viewModel.ApplyFilterAction = () =>
            {
                dataGrid.View.Filter = viewModel.GetFilter();
                dataGrid.View.RefreshFilter();
            };
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}