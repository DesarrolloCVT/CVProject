using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public class BancoModel
    {
        private int codigo;
        private string nombre;

        public int Codigo
        {
            get { return codigo; }
            set { this.codigo = value; }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }

        public BancoModel(int codigo, string nombre)
        {
            this.Codigo = codigo;
            this.Nombre = nombre;
        }
    }
}