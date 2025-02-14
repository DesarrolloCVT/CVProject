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

        InitializeComponent();
        BindingContext = new SocioViewModel(navigationService);
        var viewModel = BindingContext as SocioViewModel;
    }
}