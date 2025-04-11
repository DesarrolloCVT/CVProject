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
        private int id_banco_detalle;
        private int id_banco;
        private int codigo_banco;
        private int numero;
        private int saldo;

        public int Id_Banco_Detalle
        {
            get { return this.id_banco_detalle; }
            set { this.id_banco_detalle = value; }
        }

        public int Id_Banco
        {
            get { return this.id_banco; }
            set { this.id_banco = value; }
        }

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

        public int Saldo
        {
            get { return saldo; }
            set { this.saldo = value; }
        }

        public BancoDetalleModel(int id_banco_detalle, int id_banco, int codigo_banco, int numero, int saldo)
        {
            this.Id_Banco_Detalle = id_banco_detalle;
            this.Id_Banco = id_banco;
            this.Codigo_Banco = codigo_banco;
            this.Numero = numero;
            this.Saldo = saldo;
        }
    }
}