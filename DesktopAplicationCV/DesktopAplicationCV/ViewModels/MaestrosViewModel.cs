using DesktopAplicationCV.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.ViewModels
{
    public class MaestrosViewModel : BaseViewModel
    {
        private string _txtSocioNegocio;
        private string _txtProductos;
        private string _txtBanco;
        private string _txtTipo;
        private string _txtBancoDetalle;
        private string _txtCuentas;


        public string TxtSocioNegocio
        {
            get => _txtSocioNegocio;
            set
            {
                if (_txtSocioNegocio != value)
                {
                    _txtSocioNegocio = value;
                }
                OnPropertyChanged(nameof(TxtSocioNegocio));
            }
        }

        public string TxtProductos
        {
            get => _txtProductos;
            set
            {
                if (_txtProductos != value)
                {
                    _txtProductos = value;
                }
                OnPropertyChanged(nameof(TxtProductos));
            }
        }

        public string TxtBanco
        {
            get => _txtBanco;
            set
            {
                if (_txtBanco != value)
                {
                    _txtBanco = value;
                }
                OnPropertyChanged(nameof(TxtBanco));
            }
        }

        public string TxtTipo
        {
            get => _txtTipo;
            set
            {
                if (_txtTipo != value)
                {
                    _txtTipo = value;
                }
                OnPropertyChanged(nameof(TxtTipo));
            }
        }

        public string TxtBancoDetalle
        {
            get => _txtBancoDetalle;
            set
            {
                if (_txtBancoDetalle != value)
                {
                    _txtBancoDetalle = value;
                }
                OnPropertyChanged(nameof(TxtBancoDetalle));
            }
        }

        public string TxtCuentas
        {
            get => _txtCuentas;
            set
            {
                if (_txtCuentas != value)
                {
                    _txtCuentas = value;
                }
                OnPropertyChanged(nameof(TxtCuentas));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MaestrosViewModel()
        {
            TxtSocioNegocio = "Socio de Negocios";
            TxtProductos = "Productos";
            TxtBanco = "Banco";
            TxtTipo = "Tipo";
            TxtBancoDetalle = "Banco Detalle";
            TxtCuentas = "Cuentas";
        }
    }
}
