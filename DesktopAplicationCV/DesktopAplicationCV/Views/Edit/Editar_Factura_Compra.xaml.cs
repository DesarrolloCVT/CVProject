using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Factura_Compra : ContentPage
{
	public Editar_Factura_Compra(object obj)
	{
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();

        InitializeComponent();
        BindingContext = new FacturaCompraViewModel(navigationService, auxService);
        var viewModel = BindingContext as FacturaCompraViewModel;

        FacturaCompraModel facturaCompraModel = (FacturaCompraModel)obj;
        EditFolio.Text = facturaCompraModel.Folio.ToString().Trim();
        PkrProveedor.SelectedItem = facturaCompraModel.Proveedor.Trim();
        PkrFecha.Date = facturaCompraModel.Fecha;
        PkrMoneda.SelectedItem = facturaCompraModel.Moneda.Trim();
    }
}