using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using Syncfusion.Maui.Core.Carousel;
using Syncfusion.Maui.DataGrid;

namespace DesktopAplicationCV.Views;

public partial class Factura_Compra_Detalle : ContentPage
{
    public int total = 0;

	public Factura_Compra_Detalle(int id)
	{
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();

        InitializeComponent();
        BindingContext = new FacturaCompraDetalleViewModel(navigationService, auxService);

        var viewModel = BindingContext as FacturaCompraDetalleViewModel;
        viewModel.Id_Factura_Compra = id;

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

    /*private void CalcularValorTotal()
    {
        dataGrid.QueryUnboundColumnValue += (sender, e) =>
        {
            var datosFactCrompraDetalle = e.Record as FacturaCompraDetalleModel;

            total = 0;
            if(datosFactCrompraDetalle.Folio == 101215)
            {
                total += (datosFactCrompraDetalle.Cantidad * datosFactCrompraDetalle.Precio);
            }
        };
    }*/
}