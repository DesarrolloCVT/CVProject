using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class MonedasService : ApiService
    {
        private const string Endpoint = "monedas";

        public async Task<List<MonedaModel>> GetMonedasAsync()
        {
            return await GetAsync<List<MonedaModel>>(Endpoint) ?? new List<MonedaModel>();
        }

        public async Task<bool> AddMonedasAsync(MonedaModel moneda)
        {
            return await PostAsync(Endpoint, moneda);
        }

        public async Task<bool> UpdateMonedasAsync(MonedaModel moneda)
        {
            return await PutAsync($"{Endpoint}/{moneda.Id_Monedas}", moneda);
        }

        public async Task<bool> DeleteMonedasAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
