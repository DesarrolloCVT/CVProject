using Azure.Core;
using DesktopAplicationCV.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly SecureStorageService _secureStorageService;

        public AuthService(IHttpClientFactory httpClientFactory, SecureStorageService secureStorageService)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _secureStorageService = secureStorageService;
        }
        
        public async Task<string?> LoginAsync(string usuario, string password)
        {
            var loginRequest = new LoginRequest { Usuario = usuario, Password = password };
            var response = await _httpClient.PostAsJsonAsync("auth/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                var token = loginResponse?.Token;
                await _secureStorageService.SaveTokenAsync(token);

                return loginResponse?.Token;
            }
            return null;
        }

        public async Task<bool> LogoutAsync()
        {
            try
            {
                var token = await _secureStorageService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("⚠️ No hay token almacenado.");
                    return false;
                }

                Console.WriteLine("#Token: " + token);

                // Agregar el token al header de autorización
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                var request = new HttpRequestMessage(HttpMethod.Post, "auth/logout");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                /* Codigos de Obsoleto
                Enviar la solicitud de logout a la API
                var response = await _httpClient.PostAsync("auth/logout", null);

                var responseContent = await response.Content.ReadAsStringAsync();
                */


                Console.WriteLine($"Status: {response.StatusCode}");
                Console.WriteLine($"Response Body: {responseContent}");
                Console.WriteLine($"Headers: {response.Headers}");

                if (response.IsSuccessStatusCode)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = null;
                    Console.WriteLine("✅ Sesión cerrada correctamente en la API.");
                    _secureStorageService.RemoveToken(); // Eliminar token localmente
                    return true;
                }
                else
                {
                    Console.WriteLine($"🔴 Error al cerrar sesión: {response.StatusCode}");
                    return false;
                }

                /* Codigos de Obsoleto 
                try
                {
                    var token = await _secureStorageService.GetTokenAsync();
                    Console.WriteLine($"Token enviado: {token}");

                    if (string.IsNullOrEmpty(token))
                    {
                        Console.WriteLine("⚠️ No hay token almacenado.");
                        return false;
                    }

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await _httpClient.PostAsync("auth/logout", null);
                    Console.WriteLine(response.ToString());

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("✅ Sesión cerrada correctamente en la API.");
                        _secureStorageService.RemoveToken(); // Borra el token en el dispositivo
                        return true;
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error en logout: {response.StatusCode}, {errorMessage}");
                        Console.WriteLine($"🔴 Error al cerrar sesión: {response.StatusCode}");
                        return false;
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("LogoutAsync: " + ex.Message);
                }
                return false;
                */
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error LogoutAsync AuthService: " + Ex.Message);
                Application.Current.MainPage.DisplayAlert("Error", "Se ha producido un error no identificado", "Ok");
                return false;
            }
        }
    }
}
