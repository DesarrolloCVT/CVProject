﻿using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Services
{
    public class FacturaVentaService : ApiService
    {
        private const string Endpoint = "facturaVenta";

        public async Task<List<FacturaVentaModel>> GetFactVentaAsync()
        {
            return await GetAsync<List<FacturaVentaModel>>(Endpoint) ?? new List<FacturaVentaModel>();
        }

        public async Task<bool> AddFactVentaAsync(FacturaVentaModel facturaVentaModel)
        {
            return await PostAsync(Endpoint, facturaVentaModel);
        }

        public async Task<bool> UpdateFactVentaAsync(FacturaVentaModel facturaVentaModel)
        {
            return await PutAsync($"{Endpoint}/{facturaVentaModel.Folio}", facturaVentaModel);
        }

        public async Task<bool> DeleteFactVentaAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
