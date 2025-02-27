using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Compra_Venta : Shell
{
	public Compra_Venta(LoginViewModel loginViewModel)
	{
        InitializeComponent();
        BindingContext = loginViewModel;
        //this.Navigated += OnTabChanged;
    }

    /*private void OnTabChanged(object? sender, ShellNavigatedEventArgs e)
    {

        string NombrePagina = CurrentPage.Title;
        Console.WriteLine($"Pestaña cambiada a: {NombrePagina}");
        
    }*/
}