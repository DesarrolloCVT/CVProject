namespace DesktopAplicationCV.Views;

public partial class Socio_Negocio : ContentPage
{
	public Socio_Negocio()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		DisplayAlert("Alerta", "Se ingresaran los datos", "OK");
    }
}