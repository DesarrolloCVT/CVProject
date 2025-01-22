using DesktopAplicationCV.Models;
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
            builder.Services.AddSingleton<EditarViewModel>();
            builder.Services.AddSingleton<ProductosViewModel>();

            // Registrar Páginas
            builder.Services.AddTransient<Socio_Negocio>();
            builder.Services.AddTransient<Views.Productos>();
            builder.Services.AddTransient<EditarViews_Socio_Negocio>();
            builder.Services.AddTransient<Agregar_Socio_Negocio>();

            // Registrar servicios
            builder.Services.AddSingleton<INavigationService, NavigationService>();

            return builder.Build();
        }
    }
}
