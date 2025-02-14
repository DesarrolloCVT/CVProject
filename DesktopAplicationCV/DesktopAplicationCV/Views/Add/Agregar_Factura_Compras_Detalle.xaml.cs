using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Factura_Compras_Detalle : ContentPage
{
	public Agregar_Factura_Compras_Detalle()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new FacturaCompraDetalleViewModel(navigationService);
        var viewModel = BindingContext as FacturaCompraDetalleViewModel;
    }
}