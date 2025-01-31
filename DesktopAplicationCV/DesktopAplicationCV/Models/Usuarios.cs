using System.ComponentModel.DataAnnotations;

namespace DesktopAplicationCV.Models
{
    public class Usuarios
    {
        [Key]
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string uUsuario { get; set; }
        public string ClaveEncriptada { get; set; }
        public int Clave { get; set; }
    }
}