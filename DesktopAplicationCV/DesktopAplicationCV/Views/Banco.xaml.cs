namespace DesktopAplicationCV.Views;

public partial class Banco : ContentPage
{
	public Banco()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Alerta", "Se ingresaran los datos", "OK");
    }
}