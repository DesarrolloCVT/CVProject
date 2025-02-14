using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public class TipoModel
    {
        private int codigo;
        private string nombre;
        private string tipo_dato;
        private string cuenta;
        private int pago_factura;
        private int gasto_comercializacion;
        private int comisiones;
        private int gasto_financiero;
        private int anticipo;

        private ICommand buttonCommand;

        public int Codigo
        {
            get { return this.codigo; }
            set { this.codigo = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { this.nombre = value; }
        }

        public string Tipo_Dato
        {
            get { return tipo_dato; }
            set { this.tipo_dato = value; }
        }

        public string Cuenta
        {
            get { return cuenta; }
            set { this.cuenta = value; }
        }

        public int Pago_Factura
        {
            get { return pago_factura; }
            set { this.pago_factura = value; }
        }

        public int Gasto_Comercializacion
        {
            get { return gasto_comercializacion; }
            set { this.gasto_comercializacion = value; }
        }

        public int Comisiones
        {
            get { return comisiones; }
            set { this.comisiones = value; }
        }

        public int Gasto_Financiero
        {
            get { return gasto_financiero; }
            set { this.gasto_financiero = value; }
        }

        public int Anticipo
        {
            get { return anticipo; }
            set { this.anticipo = value; }
        }

        public TipoModel(int codigo, string nombre, string tipo_dato, string cuenta,
            int pago_factura, int gasto_comercializacion, int comisiones, int gasto_financiero, int anticipo)
        {
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Tipo_Dato = tipo_dato;
            this.Cuenta = cuenta;
            this.Pago_Factura = pago_factura;
            this.Gasto_Comercializacion = gasto_comercializacion;
            this.Comisiones = comisiones;
            this.Gasto_Financiero = gasto_financiero;
            this.Anticipo = anticipo;
        }
    }
}
