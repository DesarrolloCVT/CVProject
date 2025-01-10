namespace DesktopAplicationCV.Views;

public partial class Ingresos_Detalle : ContentPage
{
	public Ingresos_Detalle()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Alerta", "Se ingresaran los datos", "OK");
    }
}