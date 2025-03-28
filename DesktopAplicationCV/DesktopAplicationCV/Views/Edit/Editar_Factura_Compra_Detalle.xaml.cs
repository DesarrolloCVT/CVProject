using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Factura_Compra_Detalle : ContentPage
{
	public Editar_Factura_Compra_Detalle(object obj)
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new FacturaCompraDetalleViewModel(navigationService);
        var viewModel = BindingContext as FacturaCompraDetalleViewModel;

        FacturaCompraDetalleModel facturaCompraDetalleModel = (FacturaCompraDetalleModel)obj;
        EditId.Text = facturaCompraDetalleModel.Id_Factura_Compra_Detalle.ToString();
        EditFolio.Text = facturaCompraDetalleModel.Folio.ToString().Trim();
        EditCodigoProducto.Text = facturaCompraDetalleModel.Codigo_Producto.Trim();
        EditCantidad.Text = facturaCompraDetalleModel.Cantidad.ToString().Trim();
        EditPrecio.Text = facturaCompraDetalleModel.Precio.ToString().Trim();
    }
}