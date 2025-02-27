using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class MenuPrincipal : TabbedPage
{
    public MenuPrincipal(MenuPrincipalViewModel viewModel, LoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        viewModel.AgregarTabs(this, loginViewModel); // Agregar pesta�as desde el ViewModel
        //this.CurrentPageChanged += MainTabbedPage_CurrentPageChanged;
    }

    private void MainTabbedPage_CurrentPageChanged(object sender, EventArgs e)
    {
        // Obtener la pesta�a actual
        var paginaActual = this.CurrentPage;

        // Mostrar el t�tulo de la pesta�a actual en la consola
        Console.WriteLine($"Cambiado a la pesta�a: {paginaActual.Title}");
    }
}