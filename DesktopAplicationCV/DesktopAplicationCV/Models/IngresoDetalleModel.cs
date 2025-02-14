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
        private int folio_FacturaVenta;
        private int monto;

        private ICommand buttonCommand;

        public int Folio_FacturaVenta
        {
            get { return this.folio_FacturaVenta; }
            set { this.folio_FacturaVenta = value; }
        }

        public int Monto
        {
            get { return monto; }
            set { this.monto = value; }
        }

        public IngresoDetalleModel(int folio_FacturaVenta, int monto)
        {
            this.Folio_FacturaVenta = folio_FacturaVenta;
            this.Monto = monto;
        }
    }   
}
