using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using Syncfusion.Maui.DataGrid;

namespace DesktopAplicationCV.Views;

public partial class Factura_Compra : ContentPage
{
	public Factura_Compra()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new FacturaCompraViewModel(navigationService);

        var viewModel = BindingContext as FacturaCompraViewModel;

        if (viewModel != null)
        {
            // Vincular la acci�n para aplicar el filtro
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
                
                if(datos.Folio == 101215)
                {
                    e.Value = 225000;
                }
                else if(datos.Folio == 11222)
                {
                    e.Value = 500000;
                }
            }

        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}