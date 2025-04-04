using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Editar_Metodo_Pago : ContentPage
{
	public Editar_Metodo_Pago(object obj)
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new MetodoPagoViewModel(navigationService);
        var viewModel = BindingContext as MetodoPagoViewModel;

        MetodoPagoModel metodoPagoModel = (MetodoPagoModel)obj;
        EditNombre.Text = metodoPagoModel.Nombre.Trim();
    }
}