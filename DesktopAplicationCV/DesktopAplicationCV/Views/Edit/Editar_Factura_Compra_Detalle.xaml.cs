using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Factura_Compra_Detalle : ContentPage
{
	public Editar_Factura_Compra_Detalle(object obj)
	{
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();

        InitializeComponent();
        BindingContext = new FacturaCompraDetalleViewModel(navigationService, auxService);
        var viewModel = BindingContext as FacturaCompraDetalleViewModel;

        FacturaCompraDetalleModel facturaCompraDetalleModel = (FacturaCompraDetalleModel)obj;
        EditFolio.Text = facturaCompraDetalleModel.Folio.ToString().Trim();
        PkrProducto.SelectedItem = facturaCompraDetalleModel.Codigo_Producto.Trim();
        EditCantidad.Text = facturaCompraDetalleModel.Cantidad.ToString().Trim();
        EditPrecio.Text = facturaCompraDetalleModel.Precio.ToString().Trim();
    }
}