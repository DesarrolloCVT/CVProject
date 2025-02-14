using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Factura_Compra : ContentPage
{
	public Editar_Factura_Compra(object obj)
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new FacturaCompraViewModel(navigationService);
        var viewModel = BindingContext as FacturaCompraViewModel;

        FacturaCompraModel facturaCompraModel = (FacturaCompraModel)obj;
        EditFolio.Text = facturaCompraModel.Folio.ToString().Trim();
        EditProveedor.Text = facturaCompraModel.Proveedor.Trim();
        PkrFecha.Date = facturaCompraModel.Fecha;
        EditMoneda.Text = facturaCompraModel.Moneda.Trim();
    }
}