using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Transacciones_Detalle : ContentPage
{
	public Agregar_Transacciones_Detalle()
	{
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();

        InitializeComponent();
        BindingContext = new TransaccionesDetalleViewModel(navigationService, auxService);
        var viewModel = BindingContext as TransaccionesDetalleViewModel;
    }
}