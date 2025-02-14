using CommunityToolkit.Mvvm.Input;
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
    public class LoginViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly INavigationService _navigationService;
        private string _usuario;
        private string _password;

        public string Usuario
        {
            get => _usuario;
            set => SetProperty(ref _usuario, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        //public ICommand LoginCommand { get; }

        public ICommand LoginCommand => new AsyncRelayCommand(LoginAsync);

        public LoginViewModel(AuthService authService, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _authService = authService;
            //LoginCommand = new Command(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            var token = await _authService.LoginAsync(Usuario, Password);

            if (token != null)
            {
                // Guardar el token para futuras peticiones
                Preferences.Set("AuthToken", token);
                //await App.Current.MainPage.DisplayAlert("Éxito", "Inicio de sesión exitoso", "OK");
                _navigationService.NavigationAsync(new MenuPrincipal());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Credenciales inválidas", "OK");
            }
        }
    }
}