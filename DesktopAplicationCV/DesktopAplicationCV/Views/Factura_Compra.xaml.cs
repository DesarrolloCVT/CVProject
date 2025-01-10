namespace DesktopAplicationCV.Views;

public partial class Factura_Compra : ContentPage
{
	public Factura_Compra()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Alerta", "Se ingresaran los datos", "OK");
    }
}