using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Editar_Monedas : ContentPage
{
	public Editar_Monedas(object obj)
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new MonedasViewModel(navigationService);
        var viewModel = BindingContext as MonedasViewModel;

        MonedaModel monedaModel = (MonedaModel)obj;
        EditNombre.Text = monedaModel.Nombre.Trim();
    }
}