using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class TransaccionService : ApiService
    {
        private const string Endpoint = "transacciones";

        public async Task<List<TransaccionesModel>> GetTransaccionesAsync()
        {
            return await GetAsync<List<TransaccionesModel>>(Endpoint) ?? new List<TransaccionesModel>();
        }

        public async Task<bool> AddTransaccionAsync(TransaccionesModel transacciones)
        {
            return await PostAsync(Endpoint, transacciones);
        }

        public async Task<bool> UpdateTransaccionAsync(TransaccionesModel transacciones)
        {
            return await PutAsync($"{Endpoint}/{transacciones.Id_Transaccion}", transacciones);
        }

        public async Task<bool> DeleteTransaccionAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
