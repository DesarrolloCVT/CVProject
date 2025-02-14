namespace DesktopAplicationCV.Views;

public partial class MenuPrincipal : TabbedPage
{
	public MenuPrincipal()
	{
        InitializeComponent();

        this.CurrentPageChanged += MainTabbedPage_CurrentPageChanged;
    }

    private void MainTabbedPage_CurrentPageChanged(object sender, EventArgs e)
    {
        // Obtener la pestaña actual
        var paginaActual = this.CurrentPage;

        // Mostrar el título de la pestaña actual en la consola
        Console.WriteLine($"Cambiado a la pestaña: {paginaActual.Title}");
    }
}