using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Factura_Venta_Detalle : ContentPage
{
	public Agregar_Factura_Venta_Detalle()
	{
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();

        InitializeComponent();
        BindingContext = new FacturaVentaDetalleViewModel(navigationService, auxService);
        var viewModel = BindingContext as FacturaVentaDetalleViewModel;
    }
}