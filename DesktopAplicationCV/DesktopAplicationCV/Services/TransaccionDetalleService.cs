using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class TransaccionDetalleService : ApiService
    {
        private const string Endpoint = "transacciondetalle";

        public async Task<List<TransaccionesDetalleModel>> GetTransaccionesDetalleAsync(int id)
        {
            return await GetByIdAsync<List<TransaccionesDetalleModel>>(Endpoint, id) ?? new List<TransaccionesDetalleModel>();
        }

        public async Task<List<TransaccionesDetalleModel>> GetTransaccionDetalleFilterByIdAsync(int id)
        {
            var Endpoint = $"TransaccionDetalle/GetTransaccion?idTransaccion";
            return await GetFilterByIdAsync<List<TransaccionesDetalleModel>>(Endpoint, id) ?? new List<TransaccionesDetalleModel>();
        }

        public async Task<bool> AddTransaccionDetalleAsync(TransaccionesDetalleModel transaccionesDetalle)
        {
            return await PostAsync(Endpoint, transaccionesDetalle);
        }

        public async Task<bool> UpdateTransaccionAsync(TransaccionesDetalleModel transaccionesDetalle)
        {
            return await PutAsync($"{Endpoint}/{transaccionesDetalle.Id_Transaccion_Detalle}", transaccionesDetalle);
        }

        public async Task<bool> DeleteTransaccionDetalleAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
