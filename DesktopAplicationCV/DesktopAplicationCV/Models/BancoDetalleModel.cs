using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public class BancoDetalleModel
    {
        private int codigo_banco;
        private int numero;

        private ICommand buttonCommand;

        public int Codigo_Banco
        {
            get { return this.codigo_banco; }
            set { this.codigo_banco = value; }
        }

        public int Numero
        {
            get { return numero; }
            set { this.numero = value; }
        }

        public BancoDetalleModel(int codigo_banco, int numero)
        {
            this.Codigo_Banco = codigo_banco;
            this.Numero = numero;
        }
    }
}