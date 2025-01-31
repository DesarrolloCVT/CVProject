using CommunityToolkit.Maui;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.Views;
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

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.ConfigureSyncfusionCore();

            
            //API
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("http://localhost:44302/api/");
            });

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
