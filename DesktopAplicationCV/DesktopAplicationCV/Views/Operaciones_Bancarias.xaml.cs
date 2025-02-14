namespace DesktopAplicationCV.Views;

public partial class Operaciones_Bancarias : Shell
{
	public Operaciones_Bancarias()
	{
        InitializeComponent();

        this.Navigated += OnTabChanged;
    }

    private void OnTabChanged(object? sender, ShellNavigatedEventArgs e)
    {
        // Obtener la ruta actual
        string rutaActual = e.Current.Location.ToString();
        Console.WriteLine($"Pestaña cambiada a: {rutaActual}");


        var paginaActual = Shell.Current.CurrentItem;
        Console.WriteLine($"Pestaña activa: {paginaActual.Title}");
    }

}