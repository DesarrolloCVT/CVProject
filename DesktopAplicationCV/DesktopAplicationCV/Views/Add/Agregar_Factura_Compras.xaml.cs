using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Factura_Compras : ContentPage
{
	public Agregar_Factura_Compras()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new FacturaCompraViewModel(navigationService);
        var viewModel = BindingContext as FacturaCompraViewModel;

        PkrFecha.Date = DateTime.Now;

    }
}