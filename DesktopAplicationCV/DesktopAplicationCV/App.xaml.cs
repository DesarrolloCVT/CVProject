using DesktopAplicationCV.Models;
using DesktopAplicationCV.Views;

namespace DesktopAplicationCV
{
    public partial class App : Application
    {
        public App(INavigationService navigationService)
        {
            InitializeComponent();

            // Crear una NavigationPage
            var navigationPage = new NavigationPage(new Socio_Negocio());

            // Configurar la NavigationPage en el servicio
            if (navigationService is NavigationService navService)
            {
                navService.SetNavigationPage(navigationPage);
            }

            // Registrar las páginas
            navigationService.Configure("SocioNegocio", typeof(Socio_Negocio));
            navigationService.Configure("EditorSocio", typeof(Editar_Socio_Negocio));

            MainPage = navigationPage;
        }
    }
}
