using System.ComponentModel.DataAnnotations;

namespace DesktopAplicationCV.Models
{
    public class Usuario
    {
        [Key]
        public string IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ClaveEncriptada { get; set; }
        public int Clave { get; set; }
    }
}