using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Operaciones_Bancarias : Shell
{
	public Operaciones_Bancarias()
	{
        InitializeComponent();
        BindingContext = new OperacionesBancariasViewModel();
        //this.Navigated += OnTabChanged;
    }

    private void OnTabChanged(object? sender, ShellNavigatedEventArgs e)
    {
        string NombrePagina = CurrentPage.Title;
        Console.WriteLine($"Pestaña cambiada a: {NombrePagina}");
    }

}