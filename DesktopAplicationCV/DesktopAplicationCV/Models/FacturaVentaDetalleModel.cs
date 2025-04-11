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
        private int id_factura_venta_detalle;
        private int id_factura_venta;
        private int folio;
        private string codigo_producto;
        private int cantidad;
        private int precio;

        private ICommand buttonCommand;


        public int Id_Factura_Venta_Detalle
        {
            get { return this.id_factura_venta_detalle; }
            set { this.id_factura_venta_detalle = value; }
        }

        public int Id_Factura_Venta
        {
            get { return this.id_factura_venta; }
            set { this.id_factura_venta = value; }
        }

        public int Folio
        {
            get { return this.folio; }
            set { this.folio = value; }
        }

        public string Codigo_Producto
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

        public FacturaVentaDetalleModel(int id_factura_venta_detalle, int id_factura_venta, int folio, string codigo_producto, int cantidad, int precio)
        {
            this.Id_Factura_Venta_Detalle = id_factura_venta_detalle;
            this.Id_Factura_Venta = id_factura_venta;
            this.Folio = folio;
            this.Codigo_Producto = codigo_producto;
            this.Cantidad = cantidad;
            this.Precio = precio;
        }
    }
}