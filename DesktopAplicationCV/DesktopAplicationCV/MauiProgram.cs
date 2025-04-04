using CommunityToolkit.Maui;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.ViewModels;
using DesktopAplicationCV.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using Syncfusion.Maui.Core.Hosting;
using Windows.Graphics;

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
                .ConfigureSyncfusionCore()
                .UseMauiCommunityToolkit()
                .ConfigureLifecycleEvents(events =>
                {
#if WINDOWS
                    events.AddWindows(windows =>
                    {
                        windows.OnWindowCreated(window =>
                        {
                            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
                            AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

                            // Obtener el tamaño de la pantalla principal
                            DisplayArea displayArea = DisplayArea.GetFromWindowId(windowId, DisplayAreaFallback.Primary);
                            int screenWidth = displayArea.WorkArea.Width;
                            int screenHeight = displayArea.WorkArea.Height;

                            // Calcular 3/4 de la pantalla
                            int width = (int)(screenWidth * 0.75);
                            int height = (int)(screenHeight * 0.75);

                            // Aplicar el tamaño calculado
                            appWindow.Resize(new SizeInt32(width, height));

                            // Evitar que el usuario redimensione o maximice
                            OverlappedPresenter presenter = appWindow.Presenter as OverlappedPresenter;
                            if (presenter != null)
                            {
                                presenter.IsResizable = false;
                                presenter.IsMaximizable = false;
                            }
                        });
                    });
                })
                    #region Codigo obsoleta 
                    /*events.AddWindows(windows =>
                    {
                        windows.OnWindowCreated(window =>
                        {
                            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
                            AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

                            // Establecer tamaño fijo (ejemplo: 800x600)
                            appWindow.Resize(new SizeInt32(800, 600));

                            // Evitar que el usuario redimensione la ventana
                            OverlappedPresenter presenter = appWindow.Presenter as OverlappedPresenter;
                            if (presenter != null)
                            {
                                presenter.IsResizable = false;  // Desactiva el redimensionamiento
                                presenter.IsMaximizable = false; // Desactiva el botón de maximizar
                            }
                        });
                    });*/
                    #endregion
#endif              
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
                //client.BaseAddress = new Uri("https://localhost:8443/api/");
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            // Registrar Singleton
            builder.Services.AddSingleton<SocioViewModel>();
            builder.Services.AddSingleton<NavigationViewModel>();
            builder.Services.AddSingleton<ProductosViewModel>();
            builder.Services.AddSingleton<TipoViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<SecureStorageService>();
            builder.Services.AddSingleton<App>();
            builder.Services.AddSingleton<UsuarioService>();
            builder.Services.AddSingleton<MenuPrincipalViewModel>();
            builder.Services.AddSingleton<MenuPrincipal>();

            // Registrar Transient
            builder.Services.AddTransient<Socio_Negocio>();
            builder.Services.AddTransient<Productos>();
            builder.Services.AddTransient<Tipo>();
            builder.Services.AddTransient<Editar_Socio_Negocio>();
            builder.Services.AddTransient<Editar_Productos>();
            builder.Services.AddTransient<Agregar_Socio_Negocio>();
            builder.Services.AddTransient<Agregar_Productos>();
            builder.Services.AddTransient<Login>();
            builder.Services.AddTransient<LoginViewModel>();

            var app = builder.Build();
            Services = app.Services;

            return app;
        }
    }
}
