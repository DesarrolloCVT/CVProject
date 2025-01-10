namespace DesktopAplicationCV.Views;

public partial class Tipo : ContentPage
{
	public Tipo()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Alerta", "Se ingresaran los datos", "OK");
    }
}