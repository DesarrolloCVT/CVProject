using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public class CuentasModel
    {
        private int codigo;
        private string nombre;
        private string tipo;

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

        public CuentasModel(int codigo, string nombre, string tipo)
        {
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Tipo = tipo;
        }
    }
}