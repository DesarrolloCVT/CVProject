using DesktopAplicationCV.Models;
using DesktopAplicationCV.Views;
using DesktopAplicationCV.Views.Add;
using DesktopAplicationCV.Views.Edit;

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
            MainPage = new NavigationPage(new Views.Productos());
        }
    }
}
