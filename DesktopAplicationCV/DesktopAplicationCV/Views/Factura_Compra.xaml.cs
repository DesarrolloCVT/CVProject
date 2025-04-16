using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using Syncfusion.Maui.DataGrid;
using System.Collections.ObjectModel;

namespace DesktopAplicationCV.Views;

public partial class Factura_Compra : ContentPage
{
    public Factura_Compra()
	{
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();

        InitializeComponent();
        BindingContext = new FacturaCompraViewModel(navigationService, auxService);

        var viewModel = BindingContext as FacturaCompraViewModel;

        if (viewModel != null)
        {
            // Vincular la acción para aplicar el filtro
            viewModel.ApplyFilterAction = () =>
            {
                dataGrid.View.Filter = viewModel.GetFilter();
                dataGrid.View.RefreshFilter();
            };
        }


        dataGrid.QueryUnboundColumnValue += (sender, e) =>
        {
            if (e.Column.MappingName == "Total")
            {
                var datos = e.Record as FacturaCompraModel;
                e.Value = 650000;

                //Console.WriteLine("Precio: " + FacturaCompraDetalleModels.);
                //Console.WriteLine("Cantidad: " + facturaCompraDetalleViewModel.EditCantidadFactCompraDetalle);

                /*if(datos.Folio == 101215)
                {
                    e.Value = 225000;
                }
                else if(datos.Folio == 11222)
                {
                    e.Value = 500000;
                }*/
            }
        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}