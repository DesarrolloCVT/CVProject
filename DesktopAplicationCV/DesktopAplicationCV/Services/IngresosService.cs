using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class IngresosService : ApiService
    {
        private const string Endpoint = "ingresos";

        public async Task<List<IngresosModel>> GetIngresosAsync()
        {
            return await GetAsync<List<IngresosModel>>(Endpoint) ?? new List<IngresosModel>();
        }

        public async Task<bool> AddIngresoAsync(IngresosModel ingreso)
        {
            return await PostAsync(Endpoint, ingreso);
        }

        public async Task<bool> UpdateIngresoAsync(IngresosModel ingreso)
        {
            return await PutAsync($"{Endpoint}/{ingreso.Folio}", ingreso);
        }

        public async Task<bool> DeleteIngresoAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
