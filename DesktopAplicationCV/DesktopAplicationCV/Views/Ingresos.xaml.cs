namespace DesktopAplicationCV.Views;

public partial class Ingresos : ContentPage
{
	public Ingresos()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Alerta", "Se ingresaran los datos", "OK");
    }
}