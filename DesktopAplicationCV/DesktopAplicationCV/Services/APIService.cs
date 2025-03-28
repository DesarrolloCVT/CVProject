using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                //BaseAddress = new Uri("https://localhost:44374/api/")
                BaseAddress = new Uri("https://localhost:8443/api/") 
            };

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        protected async Task<T?> GetAsync<T>(string endpoint)
        {
            return await _httpClient.GetFromJsonAsync<T>(endpoint, _jsonOptions);
        }

        protected async Task<T?> GetByIdAsync<T>(string endpoint, int id)
        {
            return await _httpClient.GetFromJsonAsync<T>($"{endpoint}/{id}", _jsonOptions);
        }

        protected async Task<T?> GetFilterByIdAsync<T>(string endpoint, int id)
        {
            return await _httpClient.GetFromJsonAsync<T>($"{endpoint}={id}", _jsonOptions);
        }

        protected async Task<T?> GetFilterAsync<T>(string endpoint, string param)
        {
            return await _httpClient.GetFromJsonAsync<T>($"{endpoint}={param}", _jsonOptions);
        }

        protected async Task<bool> PostAsync<T>(string endpoint, T data)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data);
            return response.IsSuccessStatusCode;
        }

        protected async Task<bool> PutAsync<T>(string endpoint, T data)
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, data);
            return response.IsSuccessStatusCode;
        }

        protected async Task<bool> DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<MonedaModel>> GetMonedasAsync()
        {
            var response = await _httpClient.GetAsync("Monedas");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<MonedaModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<MonedaModel>();
            }

            return new List<MonedaModel>();
        }

        public async Task<List<SubtiposModel>> GetSubtiposAsync()
        {
            var response = await _httpClient.GetAsync("Subtipos");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<SubtiposModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<SubtiposModel>();
            }

            return new List<SubtiposModel>();
        }

        public async Task<List<MetodoPagoModel>> GetMetodoPagoAsync()
        {
            var response = await _httpClient.GetAsync("MetodoPago");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<MetodoPagoModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<MetodoPagoModel>();
            }

            return new List<MetodoPagoModel>();
        }

        public async Task<List<BancoModel>> GetBancosAsync()
        {
            var response = await _httpClient.GetAsync("Banco");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<BancoModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<BancoModel>();
            }

            return new List<BancoModel>();
        }
    }
}
