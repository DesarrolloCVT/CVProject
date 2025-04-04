using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Transacciones : ContentPage
{
	public Agregar_Transacciones()
	{
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();
        InitializeComponent();
        BindingContext = new TransaccionesViewModel(navigationService, auxService);
        var viewModel = BindingContext as TransaccionesViewModel;
        PkrFecha.Date = DateTime.Now;
    }
}