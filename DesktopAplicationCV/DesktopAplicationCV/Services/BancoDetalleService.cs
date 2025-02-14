using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class BancoDetalleService : ApiService
    {
        private const string Endpoint = "bancoDetalle";

        public async Task<List<BancoDetalleModel>> GetBancoDetallesAsync()
        {
            return await GetAsync<List<BancoDetalleModel>>(Endpoint) ?? new List<BancoDetalleModel>();
        }

        public async Task<bool> AddBancoDetalleAsync(BancoDetalleModel bancoDetalleModel)
        {
            return await PostAsync(Endpoint, bancoDetalleModel);
        }

        public async Task<bool> UpdateBancoDetalleAsync(BancoDetalleModel bancoDetalleModel)
        {
            return await PutAsync($"{Endpoint}/{bancoDetalleModel.Codigo_Banco}", bancoDetalleModel);
        }

        public async Task<bool> DeleteBancoDetalleAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
