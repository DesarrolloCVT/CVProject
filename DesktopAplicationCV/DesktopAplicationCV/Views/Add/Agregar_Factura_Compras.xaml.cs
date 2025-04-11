using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Factura_Compras : ContentPage
{
	public Agregar_Factura_Compras()
	{
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();

        InitializeComponent();
        BindingContext = new FacturaCompraViewModel(navigationService, auxService);
        var viewModel = BindingContext as FacturaCompraViewModel;

        PkrFecha.Date = DateTime.Now;

    }
}