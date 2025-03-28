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
        private int id_ingreso_detalle;
        private int id_ingreso;
        private int folio_FacturaVenta;
        private int monto;

        private ICommand buttonCommand;

        public int Id_Ingreso_Detalle
        {
            get { return this.id_ingreso_detalle; }
            set { this.id_ingreso_detalle = value; }
        }

        public int Id_Ingreso
        {
            get { return this.id_ingreso; }
            set { this.id_ingreso = value; }
        }

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

        public IngresoDetalleModel(int id_ingreso_detalle, int id_ingreso, int folio_FacturaVenta, int monto)
        {
            this.Id_Ingreso_Detalle = id_ingreso_detalle;
            this.Id_Ingreso = id_ingreso;
            this.Folio_FacturaVenta = folio_FacturaVenta;
            this.Monto = monto;
        }
    }   
}
