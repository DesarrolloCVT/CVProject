using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Factura_Ventas : ContentPage
{
	public Agregar_Factura_Ventas()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new FacturaVentaViewModel(navigationService);
        var viewModel = BindingContext as FacturaVentaViewModel;
        PkrFecha.Date = DateTime.Now;
    }
}