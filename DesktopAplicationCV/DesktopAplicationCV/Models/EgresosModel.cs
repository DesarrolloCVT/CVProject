using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public class EgresosModel
    {
        private int id_egreso;
        private int folio;
        private string tipo_transaccion;
        private string subtipo_transaccion;
        private string moneda;
        private DateTime fecha;
        private string cliente;
        private string metodo_pago;
        private string banco;
        private string cuenta;

        public int Id_Egreso
        {
            get { return this.id_egreso; }
            set { this.id_egreso = value; }
        }

        public int Folio
        {
            get { return this.folio; }
            set { this.folio = value; }
        }

        public string Tipo_Transaccion
        {
            get { return tipo_transaccion; }
            set { this.tipo_transaccion = value; }
        }
        
        public string Subtipo_Transaccion
        {
            get { return subtipo_transaccion; }
            set { this.subtipo_transaccion = value; }
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

        public string Cliente
        {
            get { return cliente; }
            set { this.cliente = value; }
        }

        public string Metodo_Pago
        {
            get { return metodo_pago; }
            set { this.metodo_pago = value; }
        }

        public string Banco
        {
            get { return banco; }
            set { this.banco = value; }
        }

        public string Cuenta
        {
            get { return cuenta; }
            set { this.cuenta = value; }
        }

        public EgresosModel(int id_egreso, int folio, string tipo_transaccion, string subtipo_transaccion,  string moneda, DateTime fecha, string cliente, string metodo_pago, string banco, string cuenta)
        {
            this.Id_Egreso = id_egreso;
            this.Folio = folio;
            this.Tipo_Transaccion = tipo_transaccion;
            this.Subtipo_Transaccion = subtipo_transaccion;
            this.Moneda = moneda;
            this.Fecha = fecha;
            this.Cliente = cliente;
            this.Metodo_Pago = metodo_pago;
            this.Banco = banco;
            this.Cuenta = cuenta;
        }
    }
}
