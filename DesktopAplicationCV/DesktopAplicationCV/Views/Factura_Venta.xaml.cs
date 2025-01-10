namespace DesktopAplicationCV.Views;

public partial class Factura_Venta : ContentPage
{
	public Factura_Venta()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Alerta", "Se ingresaran los datos", "OK");
    }
}