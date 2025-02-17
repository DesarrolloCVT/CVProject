using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Maestros : Shell
{
	public Maestros()
	{
		InitializeComponent();
        BindingContext = new MaestrosViewModel();
        //this.Navigated += OnTabChanged;
    }

    private void OnTabChanged(object? sender, ShellNavigatedEventArgs e)
    {
        string NombrePagina = CurrentPage.Title;
        Console.WriteLine($"Pestaña cambiada a: {NombrePagina}");
    }
}