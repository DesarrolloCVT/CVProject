using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class MetodoPagoService : ApiService
    {
        private const string Endpoint = "metodopago";

        public async Task<List<MetodoPagoModel>> GetMetodoPagoAsync()
        {
            return await GetAsync<List<MetodoPagoModel>>(Endpoint) ?? new List<MetodoPagoModel>();
        }

        public async Task<bool> AddMetodoPagoAsync(MetodoPagoModel metodoPago)
        {
            return await PostAsync(Endpoint, metodoPago);
        }

        public async Task<bool> UpdateMetodoPagoAsync(MetodoPagoModel metodoPago)
        {
            return await PutAsync($"{Endpoint}/{metodoPago.Id_Metodo_Pago}", metodoPago);
        }

        public async Task<bool> DeleteMetodoPagoAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
