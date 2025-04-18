﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public class CuentasModel
    {
        private int id_cuenta;
        private int codigo;
        private string nombre;
        private string tipo;


        public int Id_Cuenta
        {
            get { return this.id_cuenta; }
            set { this.id_cuenta = value; }
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

        public string Tipo
        {
            get { return tipo; }
            set { this.tipo = value; }
        }

        public CuentasModel(int id_cuenta, int codigo, string nombre, string tipo)
        {
            this.Id_Cuenta = id_cuenta;
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Tipo = tipo;
        }
    }
}