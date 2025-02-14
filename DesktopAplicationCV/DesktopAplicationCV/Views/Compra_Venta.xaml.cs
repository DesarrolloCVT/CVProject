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
        // Obtener la ruta actual
        string rutaActual = e.Current.Location.ToString();
        Console.WriteLine($"Pestaña cambiada a: {rutaActual}");
    }
}