using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Licensing;

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
            MainPage = new NavigationPage(MauiProgram.Services.GetService<Login>());
            //MainPage = new NavigationPage(new Productos());

            SyncfusionLicenseProvider.RegisterLicense("MzcxNTUxM0AzMjM4MmUzMDJlMzBFSUg5a1YySWxWdXBKVlpDMkRFTjdkMUtDRWF0UkE1N2psQ2prd2R3cGVFPQ==");
        }
    }
}
