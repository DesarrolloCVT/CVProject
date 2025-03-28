﻿using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public class SocioNegocioModel
    {
        private int codigo;
        private string nombre;
        private string tipo;
        private int saldo;

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

        public int Saldo
        {
            get { return saldo; }
            set { this.saldo = value; }
        }

        public SocioNegocioModel(int codigo, string nombre, string tipo, int saldo)
        {
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Tipo = tipo;
            this.Saldo = saldo;
        }
    }
}



