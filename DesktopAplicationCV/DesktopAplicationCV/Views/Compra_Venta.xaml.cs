using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Compra_Venta : Shell
{
	public Compra_Venta()
	{
        InitializeComponent();
        BindingContext = new CompraVentaViewModel();
        //this.Navigated += OnTabChanged;
    }

    private void OnTabChanged(object? sender, ShellNavigatedEventArgs e)
    {

        string NombrePagina = CurrentPage.Title;
        Console.WriteLine($"Pestaña cambiada a: {NombrePagina}");
        
    }
}