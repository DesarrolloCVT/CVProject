using DesktopAplicationCV.Models;
using DesktopAplicationCV.Views;

namespace DesktopAplicationCV
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<INavigationService, NavigationService>();

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new MenuPrincipal());
        }
    }
}
