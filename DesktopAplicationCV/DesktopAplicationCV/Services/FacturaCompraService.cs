using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class FacturaCompraService : ApiService
    {
        private const string Endpoint = "facturaCompra";

        public async Task<List<FacturaCompraModel>> GetFacturaCompraAsync()
        {
            return await GetAsync<List<FacturaCompraModel>>(Endpoint) ?? new List<FacturaCompraModel>();
        }

        public async Task<bool> AddFacturaCompraAsync(FacturaCompraModel facturaCompra)
        {
            return await PostAsync(Endpoint, facturaCompra);
        }

        public async Task<bool> UpdateFacturaCompraAsync(FacturaCompraModel facturaCompra)
        {
            return await PutAsync($"{Endpoint}/{facturaCompra.Id_Factura_Compra}", facturaCompra);
        }

        public async Task<bool> DeleteFacturaCompraAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
