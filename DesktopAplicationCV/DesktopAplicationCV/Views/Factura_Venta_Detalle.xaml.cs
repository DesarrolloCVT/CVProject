namespace DesktopAplicationCV.Views;

public partial class Factura_Venta_Detalle : ContentPage
{
	public Factura_Venta_Detalle()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Alerta", "Se ingresaran los datos", "OK");
    }
}