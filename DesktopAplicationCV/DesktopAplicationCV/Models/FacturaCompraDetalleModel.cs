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
        private int id_factura_compra_detalle;
        private int folio;
        private string codigo_producto;
        private int cantidad;
        private int precio;

        private ICommand buttonCommand;

        public int Id_Factura_Compra_Detalle
        {
            get { return this.id_factura_compra_detalle; }
            set { this.id_factura_compra_detalle = value; }
        }

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

        public FacturaCompraDetalleModel(int id_factura_compra_detalle, int folio, string codigo_producto, int cantidad, int precio)
        {
            this.Id_Factura_Compra_Detalle = id_factura_compra_detalle;
            this.Folio = folio;
            this.Codigo_Producto = codigo_producto;
            this.Cantidad = cantidad;
            this.Precio = precio;
        }
    }
}