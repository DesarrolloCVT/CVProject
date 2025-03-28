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
        private int id_banco;
        private int codigo;
        private string nombre;

        public int Id_Banco
        {
            get { return id_banco; }
            set { this.id_banco = value; }
        }

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

        public BancoModel(int id_banco, int codigo, string nombre)
        {
            this.Id_Banco = id_banco;
            this.Codigo = codigo;
            this.Nombre = nombre;
        }
    }
}