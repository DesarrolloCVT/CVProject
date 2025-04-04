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
        private int id_subtipos { get; set; }
        private string identificador { get; set; }
        private string nombre { get; set; }

        public int Id_Subtipos
        {
            get { return this.id_subtipos; }
            set { this.id_subtipos = value; }
        }

        public string Identificador
        {
            get { return this.identificador; }
            set { this.identificador = value; }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }

        public SubtiposModel(int id_subtipos, string identificador, string nombre)
        {
            this.Id_Subtipos = id_subtipos;
            this.Identificador = identificador;
            this.Nombre = nombre;
        }
    }
}
