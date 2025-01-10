namespace DesktopAplicationCV.Views;

public partial class Productos : ContentPage
{
	public Productos()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Alerta", "Se ingresaran los datos", "OK");
    }
}