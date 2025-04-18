using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Banco_Detalle : ContentPage
{
	public Banco_Detalle(int id)
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new BancoDetalleViewModel(navigationService);

        var viewModel = BindingContext as BancoDetalleViewModel;
        viewModel.Id_Banco = id;

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