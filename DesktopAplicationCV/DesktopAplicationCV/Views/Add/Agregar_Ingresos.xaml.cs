using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Ingresos : ContentPage
{
	public Agregar_Ingresos()
	{
        INavigationService navigationService = new NavigationService();
        ApiService apiService = new ApiService();
        InitializeComponent();
        BindingContext = new IngresosViewModel(navigationService, apiService);
        var viewModel = BindingContext as IngresosViewModel;
        PkrFecha.Date = DateTime.Now;
    }
}