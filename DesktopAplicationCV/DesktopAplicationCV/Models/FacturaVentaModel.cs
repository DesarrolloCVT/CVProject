using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public class FacturaVentaModel
    {
        private int folio;
        private string cliente;
        private string direccion_despacho;
        private int moneda;
        private string fecha;

        private ICommand buttonCommand;

        public int Folio
        {
            get { return this.folio; }
            set { this.folio = value; }
        }

        public string Cliente
        {
            get { return cliente; }
            set { this.cliente = value; }
        }

        public string Direccion_Despacho
        {
            get { return direccion_despacho; }
            set { this.direccion_despacho = value; }
        }

        public int Moneda

        {
            get { return moneda;
            }
            set { this.moneda = value; }
        }

        public string Fecha

        {
            get
            {
                return fecha;
            }
            set { this.fecha = value; }
        }

        public FacturaVentaModel(int folio, string cliente, string direccion_despacho, int moneda, string fecha)
        {
            this.Folio = folio;
            this.Cliente = cliente;
            this.Direccion_Despacho = direccion_despacho;
            this.Moneda = moneda;
            this.Fecha = fecha;
        }
    }
}
