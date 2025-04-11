using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Factura_Venta_Detalle : ContentPage
{
	public Factura_Venta_Detalle(int id)
	{
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();

        InitializeComponent();
        BindingContext = new FacturaVentaDetalleViewModel(navigationService, auxService);

        var viewModel = BindingContext as FacturaVentaDetalleViewModel;

        viewModel.Id_Factura_Venta = id;

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