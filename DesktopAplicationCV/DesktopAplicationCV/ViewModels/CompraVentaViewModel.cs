using DesktopAplicationCV.ViewModel;
using Syncfusion.Maui.DataGrid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.ViewModels
{
    public class CompraVentaViewModel : BaseViewModel
    {
        private string _txtFacturaVenta;
        private string _txtFacturaVentaDetalle;
        private string _txtFacturaCompra;
        private string _txtFacturaCompraDetalle;

        public string TxtFacturaVenta
        {
            get => _txtFacturaVenta;
            set
            {
                if (_txtFacturaVenta != value)
                {
                    _txtFacturaVenta = value;
                }
                OnPropertyChanged(nameof(TxtFacturaVenta));
            }
        }

        public string TxtFacturaVentaDetalle
        {
            get => _txtFacturaVentaDetalle;
            set
            {
                if (_txtFacturaVentaDetalle != value)
                {
                    _txtFacturaVentaDetalle = value;
                }
                OnPropertyChanged(nameof(TxtFacturaVentaDetalle));
            }
        }

        public string TxtFacturaCompra
        {
            get => _txtFacturaCompra;
            set
            {
                if (_txtFacturaCompra != value)
                {
                    _txtFacturaCompra = value;
                }
                OnPropertyChanged(nameof(TxtFacturaCompra));
            }
        }

        public string TxtFacturaCompraDetalle
        {
            get => _txtFacturaCompraDetalle;
            set
            {
                if (_txtFacturaCompraDetalle != value)
                {
                    _txtFacturaCompraDetalle = value;
                }
                OnPropertyChanged(nameof(TxtFacturaCompraDetalle));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CompraVentaViewModel()
        {
            TxtFacturaVenta = "Factura de Ventas";
            TxtFacturaVentaDetalle = "Detalle de Factura de Ventas";
            TxtFacturaCompra = "Factura de Compras";
            TxtFacturaCompraDetalle = "Detalle de Factura de Compras";
        }
    }
}
