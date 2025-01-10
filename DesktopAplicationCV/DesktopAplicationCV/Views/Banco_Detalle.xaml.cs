namespace DesktopAplicationCV.Views;

public partial class Banco_Detalle : ContentPage
{
	public Banco_Detalle()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Alerta", "Se ingresaran los datos", "OK");
    }
}