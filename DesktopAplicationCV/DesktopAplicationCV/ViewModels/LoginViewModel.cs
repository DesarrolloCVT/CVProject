using Azure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Resources.Languages;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.Views;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopAplicationCV.ViewModels
{   
    public partial class LoginViewModel : BaseViewModel
    {
        #region Variables
        private readonly SecureStorageService _secureStorageService;
        private readonly AuthService _authService;
        private readonly UsuarioService _usuarioService;
        private readonly INavigationService _navigationService;
        private readonly MenuPrincipalViewModel _menuPrincipalViewModel;
        private string _usuario;
        private string _nombreUsuario;
        private string _password;

        
        private Usuarios? user;

        #endregion



        #region Inicializadores
        public string Usuario
        {
            get => _usuario;
            set => SetProperty(ref _usuario, value);
        }

        public string NombreUsuario
        {
            get => _nombreUsuario;
            set => SetProperty(ref _nombreUsuario, value);
        }

        public string Password
        {
            get => _password;


            set => SetProperty(ref _password, value);
        }
        #endregion

        #region Constructor
        public LoginViewModel(AuthService authService, INavigationService navigationService, 
            MenuPrincipalViewModel menuPrincipalViewModel,
            SecureStorageService secureStorageService, UsuarioService usuarioService)
        {
            _navigationService = navigationService;
            _authService = authService;
            _menuPrincipalViewModel = menuPrincipalViewModel;
            _secureStorageService = secureStorageService;
            _usuarioService = usuarioService;
            CleanLogin();
        }

        public ICommand LogoutCommand => new AsyncRelayCommand(LogoutAsync);
        public ICommand LoginCommand => new AsyncRelayCommand(LoginAsync);
        

        #endregion

        #region Start
        public void CleanLogin()
        {
            Usuario = string.Empty;
            Password = string.Empty;
        }
        #endregion

        #region Login
        private async Task LoginAsync()
        {

            /* Codigo Hash 
             * */
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);
            Console.WriteLine("hashedPassword: " + hashedPassword);

            try
            {
                bool isConnected = Connectivity.Current.NetworkAccess == NetworkAccess.Internet;

                if (isConnected)
                {
                    Console.WriteLine("Conectado a Internet.");

                    var token = await _authService.LoginAsync(Usuario, Password);

                    if (token != null)
                    {
                        _navigationService.NavigationAsync(new MenuPrincipal(_menuPrincipalViewModel, this));
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alerta", AppResources.ErrorCredenciales, "OK");
                    }

                    var userName = Usuario.ToLower().Trim();
                    CargarUsuario(userName);
                }
                else
                {
                    Console.WriteLine("No hay conexión a Internet.");
                    await App.Current.MainPage.DisplayAlert("Alerta", AppResources.SinConexion, "OK");
                }
            }
            catch (Exception Ex) 
            {
                await App.Current.MainPage.DisplayAlert("Alerta", AppResources.ErrorAPI, "OK");
                Console.WriteLine("Error LoginAsync LoginViewModel: " + Ex.Message);
            }
        }
        #endregion

        #region Logout
        private async Task LogoutAsync()
        {
            
            bool isLoggedOut = await _authService.LogoutAsync();
            if (isLoggedOut)
            {
                //_secureStorageService.RemoveToken();
                //Preferences.Remove("AuthToken");
                Application.Current.MainPage = new NavigationPage(new Login(this));
                //_navigationService.NavigationAsync(new Login(this));
            }
        }
        #endregion

        #region Usuarios 

        [RelayCommand]
        public async Task CargarUsuario(string name)
        {
            user = await _usuarioService.GetByNameAsync(name);
            NombreUsuario = user.NombreUsuario;
        }

        #endregion
    }
}