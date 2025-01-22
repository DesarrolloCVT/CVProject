using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public partial class IngresoDetalleModel : BaseViewModel
    {
        private int folio_factura_Ventas;
        private int monto;

        private ICommand buttonCommand;

        public int Folio_Factura_Ventas
        {
            get { return this.folio_factura_Ventas; }
            set { this.folio_factura_Ventas = value; }
        }

        public int Monto
        {
            get { return monto; }
            set { this.monto = value; }
        }

        public IngresoDetalleModel(int folio_factura_Ventas, int monto)
        {
            this.Folio_Factura_Ventas = folio_factura_Ventas;
            this.Monto = monto;
        }
    }   
}
