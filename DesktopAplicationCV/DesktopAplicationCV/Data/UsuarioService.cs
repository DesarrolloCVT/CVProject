using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using DesktopAplicationCV.Models;
using Microsoft.Extensions.Configuration;

namespace DesktopAplicationCV.Data
{
    public class UsuarioService
    {
        private readonly string _connectionString;

        public UsuarioService(IConfiguration configuration)
        {
            // Obtener la cadena de conexión desde appsettings.json
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Usuario>> ObtenerUsuariosAsync()
        {
            var usuarios = new List<Usuario>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT IdUsuario, NombreUsuario, ClaveEncriptada FROM Usuarios";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        usuarios.Add(new Usuario
                        {
                            IdUsuario = reader.GetInt32(0),
                            NombreUsuario = reader.GetString(1),
                            ClaveEncriptada = reader.GetString(2)
                        });
                    }
                }
            }

            return usuarios;
        }
    }
}
