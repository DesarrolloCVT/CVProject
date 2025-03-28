using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class EgresosService : ApiService
    {
        private const string Endpoint = "egresos";

        public async Task<List<EgresosModel>> GetEgresosAsync()
        {
            return await GetAsync<List<EgresosModel>>(Endpoint) ?? new List<EgresosModel>();
        }

        public async Task<bool> AddEgresoAsync(EgresosModel egreso)
        {
            return await PostAsync(Endpoint, egreso);
        }

        public async Task<bool> UpdateEgresoAsync(EgresosModel egreso)
        {
            return await PutAsync($"{Endpoint}/{egreso.Id_Egreso}", egreso);
        }

        public async Task<bool> DeleteEgresoAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }

        public async Task<List<SubtiposModel>> GetSubtiposFilterByIdAsync(string identificador)
        {
            var NewEndPoint = $"Subtipos/GetSubtipos?Identificador";
            return await GetFilterAsync<List<SubtiposModel>>(NewEndPoint, identificador) ?? new List<SubtiposModel>();
        }
    }
}
