using DesktopAplicationCV.Models;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Productos : ContentPage
{
	public Productos()
	{
		InitializeComponent();
        try
        {
            BindingContext = new ProductosViewModel(DependencyService.Get<INavigationService>());
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: Socio_Negocio: " + ex.Message);
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Alerta", "Se ingresaran los datos", "OK");
    }
}