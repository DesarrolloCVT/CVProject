using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class SocioNegocioService : ApiService
    {
        private const string Endpoint = "socioNegocio";

        public async Task<List<SocioNegocioModel>> GetSociosAsync()
        {
            return await GetAsync<List<SocioNegocioModel>>(Endpoint) ?? new List<SocioNegocioModel>();
        }

        public async Task<bool> AddSocioAsync(SocioNegocioModel socio)
        {
            return await PostAsync(Endpoint, socio);
        }

        public async Task<bool> UpdateSocioAsync(SocioNegocioModel socioOld, SocioNegocioModel socioNew)
        {
            return await PutAsync($"{Endpoint}/{socioOld.Id_Socio.ToString()}", socioNew);
        }

        public async Task<bool> DeleteSocioAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
