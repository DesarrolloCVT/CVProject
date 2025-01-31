using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Socio_Negocio : ContentPage
{
	INavigationService navigation;
	public Editar_Socio_Negocio()
	{
		navigation = new NavigationService();
		InitializeComponent();
		BindingContext = new NavigationViewModel(navigation);
	}

    private void ModificarButton_Clicked(object sender, EventArgs e)
    {
		DisplayAlert("Alerta", "Se modifican los datos en la BD", "OK");
    }
}