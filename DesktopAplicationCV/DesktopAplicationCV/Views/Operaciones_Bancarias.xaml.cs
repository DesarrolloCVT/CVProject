using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Operaciones_Bancarias : Shell
{
    public Operaciones_Bancarias(LoginViewModel loginViewModel)
	{
        InitializeComponent();
        BindingContext = loginViewModel;
        //this.Navigated += OnTabChanged;
    }

    /*private void OnTabChanged(object? sender, ShellNavigatedEventArgs e)
    {
        string NombrePagina = CurrentPage.Title;
        Console.WriteLine($"Pesta�a cambiada a: {NombrePagina}");
    }*/
}