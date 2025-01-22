using DesktopAplicationCV.Models;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class EditarViews_Socio_Negocio : ContentPage
{
	INavigationService navigation;
	public EditarViews_Socio_Negocio()
	{
		navigation = new NavigationService();
		InitializeComponent();
		BindingContext = new EditarViewModel(navigation);
	}

    private void ModificarButton_Clicked(object sender, EventArgs e)
    {
		DisplayAlert("Alerta", "Se modifican los datos en la BD", "OK");
    }
}