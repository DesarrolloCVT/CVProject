using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Ingresos : ContentPage
{
	public Agregar_Ingresos()
	{
        INavigationService navigationService = new NavigationService();
        InitializeComponent();
        BindingContext = new IngresosViewModel(navigationService);
        var viewModel = BindingContext as IngresosViewModel;
        PkrFecha.Date = DateTime.Now;
    }
}