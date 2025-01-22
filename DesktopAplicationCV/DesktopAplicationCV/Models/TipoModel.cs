using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public class TipoModel
    {
        private int codigo;
        private string nombre;
        private string tipo;
        private int cuenta;

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

        public string Tipo
        {
            get { return tipo; }
            set { this.tipo = value; }
        }

        public int Cuenta
        {
            get { return cuenta; }
            set { this.cuenta = value; }
        }

        public TipoModel(int codigo, string nombre, string tipo, int cuenta)
        {
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Tipo = tipo;
            this.Cuenta = cuenta;
        }
    }
}
