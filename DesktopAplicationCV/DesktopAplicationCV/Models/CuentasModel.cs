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
        private int numero;
        private string tipo;

        private ICommand buttonCommand;

        public int Codigo
        {
            get { return this.codigo; }
            set { this.codigo = value; }
        }

        public int Numero
        {
            get { return numero; }
            set { this.numero = value; }
        }

        public string Tipo
        {
            get { return tipo; }
            set { this.tipo = value; }
        }

        public CuentasModel(int codigo, int numero, string tipo)
        {
            this.Codigo = codigo;
            this.Numero = numero;
            this.Tipo = tipo;
        }
    }
}