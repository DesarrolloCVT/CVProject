namespace DesktopAplicationCV.Views;

public partial class Maestros : Shell
{
	public Maestros()
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