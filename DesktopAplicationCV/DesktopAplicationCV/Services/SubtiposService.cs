using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class SubtiposService : ApiService
    {
        private const string Endpoint = "subtipos";

        public async Task<List<SubtiposModel>> GetSubtiposAsync()
        {
            return await GetAsync<List<SubtiposModel>>(Endpoint) ?? new List<SubtiposModel>();
        }

        public async Task<bool> AddSubTipoAsync(SubtiposModel subTipo)
        {
            return await PostAsync(Endpoint, subTipo);
        }

        public async Task<bool> UpdateSubTipoAsync(SubtiposModel subTipo)
        {
            return await PutAsync($"{Endpoint}/{subTipo.Id_Subtipos}", subTipo);
        }

        public async Task<bool> DeleteSubTipoAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
