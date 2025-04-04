using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Models
{
    public class MonedaModel
    {
        private int id_monedas { get; set; }
        private string nombre { get; set; }

        public int Id_Monedas
        {
            get { return this.id_monedas; }
            set { this.id_monedas = value; }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }

        public MonedaModel(int id_monedas, string nombre)
        {
            this.Id_Monedas = id_monedas;
            this.Nombre = nombre;
        }
    }
}
