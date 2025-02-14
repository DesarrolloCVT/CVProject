using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class TipoService : ApiService
    {
        private const string Endpoint = "tipo";

        public async Task<List<TipoModel>> GetTipoAsync()
        {
            return await GetAsync<List<TipoModel>>(Endpoint) ?? new List<TipoModel>();
        }

        public async Task<bool> AddTipoAsync(TipoModel tipo)
        {
            return await PostAsync(Endpoint, tipo);
        }

        public async Task<bool> UpdateTipoAsync(TipoModel tipo)
        {
            return await PutAsync($"{Endpoint}/{tipo.Codigo}", tipo);
        }

        public async Task<bool> DeleteTipoAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
