using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class CuentasService : ApiService
    {
        private const string Endpoint = "cuentas";

        public async Task<List<CuentasModel>> GetCuentasAsync()
        {
            return await GetAsync<List<CuentasModel>>(Endpoint) ?? new List<CuentasModel>();
        }

        public async Task<bool> AddCuentaAsync(CuentasModel cuenta)
        {
            return await PostAsync(Endpoint, cuenta);
        }

        public async Task<bool> UpdateCuentaAsync(CuentasModel cuenta)
        {
            return await PutAsync($"{Endpoint}/{cuenta.Codigo}", cuenta);
        }

        public async Task<bool> DeleteCuentaAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
