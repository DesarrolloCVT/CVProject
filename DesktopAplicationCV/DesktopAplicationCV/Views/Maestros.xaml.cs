using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Maestros : Shell
{
	public Maestros(LoginViewModel loginViewModel)
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