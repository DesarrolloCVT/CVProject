using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Models
{
    public class OrderInfo
    {
        private ImageSource eliminar;
        private ImageSource editar;
        private string codigo;
        private string nombre;
        private string tipo;
        private int saldo;

        public ImageSource Eliminar
        {
            get { return eliminar; }
            set { this.eliminar = value; }
        }

        public ImageSource Editar
        {
            get { return editar; }
            set { this.editar = value; }
        }

        public string Codigo
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

        public int Saldo
        {
            get { return saldo; }
            set { this.saldo = value; }
        }

        public OrderInfo(ImageSource eliminar, ImageSource editar, string nombre, string tipo, string codigo, int saldo)
        {
            this.Eliminar = eliminar;
            this.Editar = editar;
            this.Nombre = nombre;
            this.Tipo = tipo;
            this.Codigo = codigo;
            this.Saldo = saldo;
        }
    }
}



