using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public  class FacturaCompraDetalleModel
    {
        private int codigo_producto;
        private string fecha;
        private int moneda;

        private ICommand buttonCommand;

        public int Codigo_Producto
        {
            get { return this.codigo_producto; }
            set { this.codigo_producto = value; }
        }

        public string Fecha
        {
            get { return fecha; }
            set { this.fecha = value; }
        }

        public int Moneda
        {
            get { return moneda; }
            set { this.moneda = value; }
        }

        public FacturaCompraDetalleModel(int codigo_producto, string fecha, int moneda)
        {
            this.Codigo_Producto = codigo_producto;
            this.Fecha = fecha;
            this.Moneda = moneda;
        }
    }
}