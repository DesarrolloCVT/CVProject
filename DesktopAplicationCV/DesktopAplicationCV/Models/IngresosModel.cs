using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public class IngresosModel
    {
        private int folio;
        private string tipo;
        private string moneda;
        private string fecha;
        private string cliente;
        private string metodo_pago;
        private string banco;
        private int cuenta;

        private ICommand buttonCommand;

        public int Folio
        {
            get { return this.folio; }
            set { this.folio = value; }
        }

        public string Tipo
        {
            get { return tipo; }
            set { this.tipo = value; }
        }

        public string Moneda
        {
            get { return moneda; }
            set { this.moneda = value; }
        }

        public string Fecha
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

        public int Cuenta
        {
            get { return cuenta; }
            set { this.cuenta = value; }
        }

        public IngresosModel(int folio, string tipo, string moneda, string fecha, string cliente, string metodo_pago, string banco, int cuenta)
        {
            this.Folio = folio;
            this.Tipo = tipo;
            this.Moneda = moneda;
            this.Fecha = fecha;
            this.Cliente = cliente;
            this.Metodo_Pago = metodo_pago;
            this.Banco = banco;
            this.Cuenta = cuenta;
        }
    }
}
