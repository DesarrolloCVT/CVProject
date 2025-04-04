using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class AuxService : ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private string Endpoint;

        public  AuxService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44374/api/")
                //BaseAddress = new Uri("https://localhost:8443/api/")
            };

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        /*public async Task<List<MonedaModel>> GetMonedasAsync()
        {
            Endpoint = "monedas";
            return await GetAsync<List<MonedaModel>>(Endpoint) ?? new List<MonedaModel>();
        }*/

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

        /*public async Task<List<SubtiposModel>> GetSubtiposAsync()
        {
            var response = await _httpClient.GetAsync("Subtipos");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<SubtiposModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<SubtiposModel>();
            }

            return new List<SubtiposModel>();
        }*/

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

        public async Task<List<SocioNegocioModel>> GetSociosNegocioAsync()
        {
            var response = await _httpClient.GetAsync("SocioNegocio");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<SocioNegocioModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<SocioNegocioModel>();
            }

            return new List<SocioNegocioModel>();
        }

        public async Task<List<CuentasModel>> GetCuentasAsync()
        {
            var response = await _httpClient.GetAsync("Cuentas");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<CuentasModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CuentasModel>();
            }

            return new List<CuentasModel>();
        }

        public async Task<List<TipoModel>> GetTiposAsync()
        {
            var response = await _httpClient.GetAsync("Tipo");
            if (response.IsSuccessStatusCode)
            {   
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<TipoModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<TipoModel>();
                
            }

            return new List<TipoModel>();
        }

        public async Task<List<SubtiposModel>> GetSubtiposFilterByIdAsync(string identificador)
        {
            var EndPoint = $"Subtipos/GetSubtipo?Identificador";
            return await GetFilterAsync<List<SubtiposModel>>(EndPoint, identificador) ?? new List<SubtiposModel>();
        }
    }
}
