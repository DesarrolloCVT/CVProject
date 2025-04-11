using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using Syncfusion.Maui.Core.Carousel;
using System.Collections.ObjectModel;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Socio_Negocio : ContentPage
{
    public Agregar_Socio_Negocio()
	{
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();

        InitializeComponent();
        BindingContext = new SocioViewModel(navigationService, auxService);
        var viewModel = BindingContext as SocioViewModel;
    }
}