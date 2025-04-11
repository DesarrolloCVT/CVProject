using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Models
{
    public class TransaccionesDetalleModel
    {
        private int id_transaccion_detalle;
        private int? id_transaccion;
        private int folio_factura;
        private string tipo_factura;
        private int monto;

        public int Id_Transaccion_Detalle
        {
            get { return this.id_transaccion_detalle; }
            set { this.id_transaccion_detalle = value; }
        }

        public int? Id_Transaccion
        {
            get { return this.id_transaccion; }
            set { this.id_transaccion = value; }
        }

        public int Folio_Factura
        {
            get { return this.folio_factura; }
            set { this.folio_factura = value; }
        }

        public string Tipo_Factura
        {
            get { return this.tipo_factura; }
            set { this.tipo_factura = value; }
        }

        public int Monto
        {
            get { return this.monto; }
            set { this.monto = value; }
        }

        public TransaccionesDetalleModel(int id_transaccion_detalle, int? id_transaccion, int folio_factura, string tipo_factura, int monto)
        {
            this.Id_Transaccion_Detalle = id_transaccion_detalle;
            this.Id_Transaccion = id_transaccion;
            this.Folio_Factura = folio_factura;
            this.Tipo_Factura = tipo_factura;
            this.Monto = monto;
        }
    }
}
