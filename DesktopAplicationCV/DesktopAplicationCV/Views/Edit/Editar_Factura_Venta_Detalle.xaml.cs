using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Factura_Venta_Detalle : ContentPage
{
	public Editar_Factura_Venta_Detalle(object obj)
	{
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();

        InitializeComponent();
        BindingContext = new FacturaVentaDetalleViewModel(navigationService, auxService);
        var viewModel = BindingContext as FacturaVentaDetalleViewModel;

        FacturaVentaDetalleModel facturaVentaDetalleModel = (FacturaVentaDetalleModel)obj;
        EditFolio.Text = facturaVentaDetalleModel.Folio.ToString().Trim();
        PkrProducto.SelectedItem = facturaVentaDetalleModel.Codigo_Producto.Trim();
        EditCantidad.Text = facturaVentaDetalleModel.Cantidad.ToString().Trim();
        EditPrecio.Text = facturaVentaDetalleModel.Precio.ToString().Trim();
    }
}