using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Models
{
    public class MetodoPagoModel
    {
        private int id_metodo_pago { get; set; }
        private string nombre { get; set; }

        public int Id_Metodo_Pago
        {
            get { return this.id_metodo_pago; }
            set { this.id_metodo_pago = value; }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }

        public MetodoPagoModel(int id_metodo_pago, string nombre) 
        {
            this.Id_Metodo_Pago = id_metodo_pago;
            this.Nombre = nombre;
        }
    }
}
