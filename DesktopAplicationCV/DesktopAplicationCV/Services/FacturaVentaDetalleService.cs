using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class FacturaVentaDetalleService : ApiService
    {
        private const string Endpoint = "facturaVentaDetalle";

        public async Task<List<FacturaVentaDetalleModel>> GetFactVentaDetallesAsync()
        {
            return await GetAsync<List<FacturaVentaDetalleModel>>(Endpoint) ?? new List<FacturaVentaDetalleModel>();
        }

        public async Task<List<FacturaVentaDetalleModel>> GetFactVentaDetalleFilterByIdAsync(int id)
        {
            var Endpoint = $"FacturaVentaDetalle/GetFacturaVentaDetalle?idFactVenta";
            return await GetFilterByIdAsync<List<FacturaVentaDetalleModel>>(Endpoint, id) ?? new List<FacturaVentaDetalleModel>();
        }

        public async Task<bool> AddFactVentaDetalleAsync(FacturaVentaDetalleModel facturaVentaDetalleModel)
        {
            return await PostAsync(Endpoint, facturaVentaDetalleModel);
        }

        public async Task<bool> UpdateFactVentaDetalleAsync(FacturaVentaDetalleModel facturaVentaDetalleModel)
        {
            return await PutAsync($"{Endpoint}/{facturaVentaDetalleModel.Id_Factura_Venta_Detalle}", facturaVentaDetalleModel);
        }

        public async Task<bool> DeleteFactVentaDetalleAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
