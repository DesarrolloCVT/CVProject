using DesktopAplicationCV.Data;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace DesktopAplicationCV
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            var text = FileSystem.AppDataDirectory;

            // Cargar appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(FileSystem.AppDataDirectory) // Ubicación base
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Agregar la configuración al contenedor de servicios
            builder.Configuration.AddConfiguration(config);

            builder.Services.AddSingleton<UsuarioService>();
            builder.Services.AddSingleton<UsuarioViewModel>();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.ConfigureSyncfusionCore();


            // Registrar ViewModels
            builder.Services.AddSingleton<SocioViewModel>();
            builder.Services.AddSingleton<NavigationViewModel>();
            builder.Services.AddSingleton<ProductosViewModel>();

            // Registrar Páginas
            builder.Services.AddTransient<Socio_Negocio>();
            builder.Services.AddTransient<Productos>();
            builder.Services.AddTransient<Editar_Socio_Negocio>();
            builder.Services.AddTransient<Editar_Productos>();
            builder.Services.AddTransient<Agregar_Socio_Negocio>();
            builder.Services.AddTransient<Agregar_Productos>();

            // Registrar servicios
            builder.Services.AddSingleton<INavigationService, NavigationService>();

            return builder.Build();
        }
    }
}
