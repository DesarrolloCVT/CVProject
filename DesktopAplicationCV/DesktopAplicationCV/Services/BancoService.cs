using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class BancoService : ApiService
    {
        private const string Endpoint = "banco";

        public async Task<List<BancoModel>> GetBancosAsync()
        {
            return await GetAsync<List<BancoModel>>(Endpoint) ?? new List<BancoModel>();
        }

        public async Task<bool> AddBancoAsync(BancoModel banco)
        {
            return await PostAsync(Endpoint, banco);
        }

        public async Task<bool> UpdateBancoAsync(BancoModel banco)
        {
            return await PutAsync($"{Endpoint}/{banco.Id_Banco}", banco);
        }

        public async Task<bool> DeleteBancoAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
