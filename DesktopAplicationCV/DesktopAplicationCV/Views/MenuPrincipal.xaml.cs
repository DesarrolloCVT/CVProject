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
        // Obtener la pesta�a actual
        var paginaActual = this.CurrentPage;

        // Mostrar el t�tulo de la pesta�a actual en la consola
        Console.WriteLine($"Cambiado a la pesta�a: {paginaActual.Title}");
    }
}