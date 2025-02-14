using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class IngresoDetalleService : ApiService
    {
        private const string Endpoint = "ingresosDetalle";

        public async Task<List<IngresoDetalleModel>> GetIngresoDetalleAsync()
        {
            return await GetAsync<List<IngresoDetalleModel>>(Endpoint) ?? new List<IngresoDetalleModel>();
        }

        public async Task<bool> AddIngresoDetalleAsync(IngresoDetalleModel ingresoDetalleModel)
        {
            return await PostAsync(Endpoint, ingresoDetalleModel);
        }

        public async Task<bool> UpdateIngresoDetalleAsync(IngresoDetalleModel ingresoDetalleModel)
        {
            return await PutAsync($"{Endpoint}/{ingresoDetalleModel.Folio_FacturaVenta}", ingresoDetalleModel);
        }

        public async Task<bool> DeleteIngresoDetalleAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
