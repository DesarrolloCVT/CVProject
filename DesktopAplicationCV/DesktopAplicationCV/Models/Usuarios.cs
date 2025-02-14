using System.ComponentModel.DataAnnotations;

namespace DesktopAplicationCV.Models
{
    public class Usuarios
    {
        [Key]
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string UsuarioSistema { get; set; }
        public string ClaveEncriptada { get; set; }
        public string Clave { get; set; }
    }
}