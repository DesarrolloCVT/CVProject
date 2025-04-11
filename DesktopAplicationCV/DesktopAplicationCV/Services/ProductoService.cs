using DesktopAplicationCV.Models;

namespace DesktopAplicationCV.Services
{
    public class ProductoService : ApiService
    {
        private const string Endpoint = "productos";

        public async Task<List<ProductosModel>> GetProductosAsync()
        {
            return await GetAsync<List<ProductosModel>>(Endpoint) ?? new List<ProductosModel>();
        }

        public async Task<bool> AddProductoAsync(ProductosModel producto)
        {
            return await PostAsync(Endpoint, producto);
        }

        public async Task<bool> UpdateProductoAsync(ProductosModel producto)
        {
            return await PutAsync($"{Endpoint}/{producto.Id_Producto}", producto);
        }

        public async Task<bool> DeleteProductoAsync(int id)
        {
            return await DeleteAsync($"{Endpoint}/{id}");
        }
    }
}