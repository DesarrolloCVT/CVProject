using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public  class FacturaVentaDetalleModel
    {
        private int folio;
        private int codigo_producto;
        private int cantidad;
        private int precio;

        private ICommand buttonCommand;

        public int Folio
        {
            get { return this.folio; }
            set { this.folio = value; }
        }

        public int Codigo_Producto
        {
            get { return codigo_producto; }
            set { this.codigo_producto = value; }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set { this.cantidad = value; }
        }

        public int Precio
        {
            get
            {
                return precio;
            }
            set { this.precio = value; }
        }

        public FacturaVentaDetalleModel(int folio, int codigo_producto, int cantidad, int precio)
        {
            this.Folio = folio;
            this.Codigo_Producto = codigo_producto;
            this.Cantidad = cantidad;
            this.Precio = precio;
        }
    }
}