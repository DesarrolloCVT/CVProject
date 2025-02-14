using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class FacturaCompraDetalleService : ApiService
    {
        private const string Endpoint = "facturaCompraDetalle";

        public async Task<List<FacturaCompraDetalleModel>> GetFactCompraDetallesAsync()
        {
            return await GetAsync<List<FacturaCompraDetalleModel>>(Endpoint) ?? new List<FacturaCompraDetalleModel>();
        }

        public async Task<bool> AddFactCompraDetalleAsync(FacturaCompraDetalleModel facturaCompraDetalleModel)
        {
            return await PostAsync(Endpoint, facturaCompraDetalleModel);
        }

        public async Task<bool> UpdateFactCompraDetalleAsync(FacturaCompraDetalleModel facturaCompraDetalleModel)
        {
            return await PutAsync($"{Endpoint}/{facturaCompraDetalleModel.Folio}", facturaCompraDetalleModel);
        }

        public async Task<bool> DeleteFactCompraDetalleAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
