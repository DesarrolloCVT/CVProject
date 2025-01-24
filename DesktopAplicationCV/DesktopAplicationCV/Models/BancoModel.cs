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
        private int nombre;
        private int codigo;

        private ICommand buttonCommand;

        public int Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }

        public int Codigo
        {
            get { return codigo; }
            set { this.codigo = value; }
        }

        public BancoModel(int nombre, int codigo)
        {
            this.Nombre = nombre;
            this.Codigo = codigo;
        }
    }
}