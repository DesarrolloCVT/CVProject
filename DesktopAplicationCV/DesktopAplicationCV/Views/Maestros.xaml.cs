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
        string NombrePagina = CurrentPage.Title;
        Console.WriteLine($"Pestaña cambiada a: {NombrePagina}");
    }
}