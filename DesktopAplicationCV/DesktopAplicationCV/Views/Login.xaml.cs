using Microsoft.Maui.Animations;

namespace DesktopAplicationCV.Views;

public partial class Login : ContentPage
{
    public Login()
	{
		InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        //Agregar validacion para el inicio de sesion.
        await Navigation.PushAsync(new MenuPrincipal());
    }
}