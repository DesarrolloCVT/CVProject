using DesktopAplicationCV.Models;
using DesktopAplicationCV.Views;

namespace DesktopAplicationCV
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Configuramos el servicio de navegación
            DependencyService.Register<INavigationService, NavigationService>();

            // Asignamos la página principal con un NavigationPage
            MainPage = new NavigationPage(new Socio_Negocio());
        }
    }
}
