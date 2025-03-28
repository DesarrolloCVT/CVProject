using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Models
{
    public class SubtiposModel
    {
        [Key]
        public int Id_Subtipos { get; set; }
        public string Identificador { get; set; }
        public string Nombre { get; set; }
    }
}
