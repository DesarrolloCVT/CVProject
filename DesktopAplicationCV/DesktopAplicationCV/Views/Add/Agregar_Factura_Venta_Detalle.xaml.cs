using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Factura_Venta_Detalle : ContentPage
{
	public Agregar_Factura_Venta_Detalle()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new FacturaVentaDetalleViewModel(navigationService);
        var viewModel = BindingContext as FacturaVentaDetalleViewModel;
    }
}