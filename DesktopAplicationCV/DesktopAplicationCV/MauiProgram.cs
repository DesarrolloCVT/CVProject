using CommunityToolkit.Maui;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.ViewModels;
using DesktopAplicationCV.Views;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace DesktopAplicationCV
{
    public static class MauiProgram
    {
        public static IServiceProvider Services { get; private set; }

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
                client.BaseAddress = new Uri("https://localhost:44374/api/");
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            // Registrar ViewModels
            builder.Services.AddSingleton<SocioViewModel>();
            builder.Services.AddSingleton<NavigationViewModel>();
            builder.Services.AddSingleton<ProductosViewModel>();
            builder.Services.AddSingleton<TipoViewModel>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddSingleton<CompraVentaViewModel>();
            builder.Services.AddSingleton<OperacionesBancariasViewModel>();
            builder.Services.AddSingleton<MaestrosViewModel>();


            // Registrar Páginas
            builder.Services.AddTransient<Socio_Negocio>();
            builder.Services.AddTransient<Productos>();
            builder.Services.AddTransient<Tipo>();
            builder.Services.AddTransient<Editar_Socio_Negocio>();
            builder.Services.AddTransient<Editar_Productos>();
            builder.Services.AddTransient<Agregar_Socio_Negocio>();
            builder.Services.AddTransient<Agregar_Productos>();
            builder.Services.AddTransient<Login>();

            // Registrar servicios
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<AuthService>();


            //builder.Services.AddSingleton<AuthService>();
            //builder.Services.AddTransient<LoginViewModel>();
            //builder.Services.AddTransient<Login>();
            builder.Services.AddSingleton<App>();

            var app = builder.Build();
            Services = app.Services;

            return app;
        }
    }
}
