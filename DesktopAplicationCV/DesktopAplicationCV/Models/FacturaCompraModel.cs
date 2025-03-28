using DesktopAplicationCV.Views;
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
        private int id_factura_compra;
        private int folio;
        private string proveedor;
        private DateTime fecha;
        private string moneda;

        public int Id_Factura_Compra
        {
            get { return this.id_factura_compra; }
            set { this.id_factura_compra = value; }
        }

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
        public List<FacturaCompraDetalleModel> Detalles { get; set; } = new List<FacturaCompraDetalleModel>();

        // Total calculado sumando Precio * Cantidad de los detalles
        public decimal Total => Detalles.Sum(d => d.Precio * d.Cantidad);

        public FacturaCompraModel(int id_factura_compra, int folio, string proveedor, DateTime fecha, string moneda)
        {
            this.Id_Factura_Compra = id_factura_compra;
            this.Folio = folio;
            this.Proveedor = proveedor;
            this.Fecha = fecha;
            this.Moneda = moneda;
        }
    }
}