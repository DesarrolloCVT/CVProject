namespace DesktopAplicationCV.Views;

public partial class Factura_Compra_detalle : ContentPage
{
	public Factura_Compra_detalle()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Alerta", "Se ingresaran los datos", "OK");
    }
}