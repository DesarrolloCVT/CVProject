using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public class TipoModel
    {
        private int id_tipo;
        private int codigo;
        private string nombre;
        private string tipo_dato;
        private int cuenta;

        public int Id_Tipo
        {
            get { return this.id_tipo; }
            set { this.id_tipo = value; }
        }

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

        public TipoModel(int id_tipo, int codigo, string nombre, string tipo_dato, int cuenta)
        {
            this.Id_Tipo = id_tipo;
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Tipo_Dato = tipo_dato;
            this.Cuenta = cuenta;
        }
    }
}
