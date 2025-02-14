using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Ingresos_Detalle : ContentPage
{
	public Agregar_Ingresos_Detalle()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new IngresoDetalleViewsModel(navigationService);
        var viewModel = BindingContext as IngresoDetalleViewsModel;
    }
}