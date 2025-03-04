using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<Usuarios?> GetByNameAsync(string name)
        {
            try
            {   
                var response = await _httpClient.GetAsync($"https://localhost:44374/api/usuarios/name/{name}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Usuarios>();
                }
                else
                {
                    Console.WriteLine($"🔴 Error en la solicitud: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en GetByIdAsync: {ex.Message}");
                return null;
            }
        }
    }
}
