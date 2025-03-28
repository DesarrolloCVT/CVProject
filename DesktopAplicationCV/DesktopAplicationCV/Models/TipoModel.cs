using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public class TipoModel
    {
        private int codigo;
        private string nombre;
        private string tipo_dato;
        private int cuenta;

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

        public int Cuenta
        {
            get { return cuenta; }
            set { this.cuenta = value; }
        }

        public TipoModel(int codigo, string nombre, string tipo_dato, int cuenta)
        {
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Tipo_Dato = tipo_dato;
            this.Cuenta = cuenta;
        }
    }
}
