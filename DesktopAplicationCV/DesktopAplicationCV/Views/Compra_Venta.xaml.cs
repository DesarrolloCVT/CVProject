namespace DesktopAplicationCV.Views;

public partial class Compra_Venta : Shell
{
	public Compra_Venta()
	{
        InitializeComponent();

        this.Navigated += OnTabChanged;
    }

    private void OnTabChanged(object? sender, ShellNavigatedEventArgs e)
    {

        string NombrePagina = CurrentPage.Title;
        Console.WriteLine($"Pesta�a cambiada a: {NombrePagina}");
        
    }
}