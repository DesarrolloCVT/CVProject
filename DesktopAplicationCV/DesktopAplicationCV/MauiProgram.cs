using DesktopAplicationCV.Models;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.Views;
using DesktopAplicationCV.Views.Add;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace DesktopAplicationCV
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
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
