using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Factura_Venta_Detalle : ContentPage
{
	public Editar_Factura_Venta_Detalle(object obj)
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new FacturaVentaDetalleViewModel(navigationService);
        var viewModel = BindingContext as FacturaVentaDetalleViewModel;

        FacturaVentaDetalleModel facturaVentaDetalleModel = (FacturaVentaDetalleModel)obj;
        EditFolio.Text = facturaVentaDetalleModel.Folio.ToString().Trim();
        EditCodProducto.Text = facturaVentaDetalleModel.Codigo_Producto.Trim();
        EditCantidad.Text = facturaVentaDetalleModel.Cantidad.ToString().Trim();
        EditPrecio.Text = facturaVentaDetalleModel.Precio.ToString().Trim();
    }
}