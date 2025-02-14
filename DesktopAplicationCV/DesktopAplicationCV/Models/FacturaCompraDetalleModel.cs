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
        private int folio;
        private string codigo_producto;
        private int cantidad;
        private int precio;

        private ICommand buttonCommand;

        public int Folio
        {
            get { return this.folio; }
            set { this.folio = value; }
        }

        public string Codigo_Producto
        {
            get { return this.codigo_producto; }
            set { this.codigo_producto = value; }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set { this.cantidad = value; }
        }

        public int Precio
        {
            get { return precio; }
            set { this.precio = value; }
        }

        public FacturaCompraDetalleModel(int folio, string codigo_producto, int cantidad, int precio)
        {
            this.Folio = folio;
            this.Codigo_Producto = codigo_producto;
            this.Cantidad = cantidad;
            this.Precio = precio;
        }
    }
}