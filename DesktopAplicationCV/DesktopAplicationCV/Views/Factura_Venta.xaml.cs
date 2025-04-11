using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Factura_Venta : ContentPage
{
	public Factura_Venta()
	{
        INavigationService navigationService = new NavigationService();
        AuxService auxService= new AuxService();

        InitializeComponent();
        BindingContext = new FacturaVentaViewModel(navigationService, auxService);

        var viewModel = BindingContext as FacturaVentaViewModel;

        if (viewModel != null)
        {
            // Vincular la acción para aplicar el filtro
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