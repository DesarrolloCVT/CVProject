using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Egresos : ContentPage
{
	public Agregar_Egresos()
	{
        INavigationService navigationService = new NavigationService();
        ApiService apiService = new ApiService();
        InitializeComponent();
        BindingContext = new EgresosViewModel(navigationService, apiService);
        var viewModel = BindingContext as EgresosViewModel;
        PkrFecha.Date = DateTime.Now;
    }
}