namespace DesktopAplicationCV.Views;

public partial class Cuentas : ContentPage
{
	public Cuentas()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Alerta", "Se ingresaran los datos", "OK");
    }
}