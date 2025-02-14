using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Factura_Ventas : ContentPage
{
	public Editar_Factura_Ventas(object obj)
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new FacturaVentaViewModel(navigationService);
        var viewModel = BindingContext as FacturaVentaViewModel;

        FacturaVentaModel facturaVentaModel = (FacturaVentaModel)obj;
        EditFolio.Text = facturaVentaModel.Folio.ToString().Trim();
        EditCliente.Text = facturaVentaModel.Cliente.Trim();
        EditDirDespacho.Text = facturaVentaModel.Direccion_Despacho.Trim();
        EditMoneda.Text = facturaVentaModel.Moneda.Trim();
        PkrFecha.Date = facturaVentaModel.Fecha;
    }
}