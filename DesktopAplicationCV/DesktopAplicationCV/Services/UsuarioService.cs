using DesktopAplicationCV.Models;
using System.Net.Http.Json;

namespace DesktopAplicationCV.Services
{
    public class UsuarioService
    {   
        private readonly HttpClient _httpClient;

        public UsuarioService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<List<Usuario>> ObtenerUsuariosAsync()
        {
            try
            {
                var usuarios = await _httpClient.GetFromJsonAsync<List<Usuario>>("usuarios"); // Asegúrate que el endpoint sea correcto
                return usuarios;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task AgregarUsuarioAsync(Usuario usuario)
        {
            var response = await _httpClient.PostAsJsonAsync("usuarios", usuario);
            response.EnsureSuccessStatusCode();
        }
    }
}