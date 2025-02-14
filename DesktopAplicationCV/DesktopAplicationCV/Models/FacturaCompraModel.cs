using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public  class FacturaCompraModel
    {
        private int folio;
        private string proveedor;
        private DateTime fecha;
        private string moneda;

        private ICommand buttonCommand;

        public int Folio
        {
            get { return this.folio; }
            set { this.folio = value; }
        }

        public string Proveedor
        {
            get { return proveedor; }
            set { this.proveedor = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { this.fecha = value; }
        }

        public string Moneda
        {
            get { return moneda; }
            set { this.moneda = value; }
        }

        public FacturaCompraModel(int folio, string proveedor, DateTime fecha, string moneda)
        {
            this.Folio = folio;
            this.Proveedor = proveedor;
            this.Fecha = fecha;
            this.Moneda = moneda;
        }
    }
}