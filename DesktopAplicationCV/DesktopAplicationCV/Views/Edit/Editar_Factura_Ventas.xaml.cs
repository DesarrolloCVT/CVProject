using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Factura_Ventas : ContentPage
{
    public Editar_Factura_Ventas(object obj)
    {
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();

        InitializeComponent();
        BindingContext = new FacturaVentaViewModel(navigationService, auxService);
        var viewModel = BindingContext as FacturaVentaViewModel;

        FacturaVentaModel facturaVentaModel = (FacturaVentaModel)obj;
        EditFolio.Text = facturaVentaModel.Folio.ToString().Trim();
        PkrCliente.SelectedItem = facturaVentaModel.Cliente.Trim();
        EditDirDespacho.Text = facturaVentaModel.Direccion_Despacho.Trim();
        PkrMoneda.SelectedItem = facturaVentaModel.Moneda.Trim();
        PkrFecha.Date = facturaVentaModel.Fecha;
    }
}