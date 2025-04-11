using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Factura_Compras_Detalle : ContentPage
{
	public Agregar_Factura_Compras_Detalle()
	{
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();

        InitializeComponent();
        BindingContext = new FacturaCompraDetalleViewModel(navigationService, auxService);
        var viewModel = BindingContext as FacturaCompraDetalleViewModel;
    }
}