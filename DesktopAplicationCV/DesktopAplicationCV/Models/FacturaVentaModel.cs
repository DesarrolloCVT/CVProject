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
        private int id_factura_venta;
        private int folio;
        private string cliente;
        private string direccion_despacho;
        private string moneda;
        private DateTime fecha;

        private ICommand buttonCommand;

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

        public string Moneda
        { 
            get { return moneda; }
            set { this.moneda = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { this.fecha = value; }
        }

        public FacturaVentaModel(int id_factura_venta, int folio, string cliente, string direccion_despacho, string moneda, DateTime fecha)
        {
            this.Id_Factura_Venta = id_factura_venta;
            this.Folio = folio;
            this.Cliente = cliente;
            this.Direccion_Despacho = direccion_despacho;
            this.Moneda = moneda;
            this.Fecha = fecha;
        }
    }
}
