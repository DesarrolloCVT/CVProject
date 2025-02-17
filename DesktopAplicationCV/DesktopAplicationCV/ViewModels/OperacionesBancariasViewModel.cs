using DesktopAplicationCV.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.ViewModels
{
    public class OperacionesBancariasViewModel : BaseViewModel
    {
        private string _txtIngreso;
        private string _txtIngresosDetalles;


        public string TxtIngresos
        {
            get => _txtIngreso;
            set
            {
                if (_txtIngreso != value)
                {
                    _txtIngreso = value;
                }
                OnPropertyChanged(nameof(TxtIngresos));
            }
        }

        public string TxtIngresosDetalles
        {
            get => _txtIngresosDetalles;
            set
            {
                if (_txtIngresosDetalles != value)
                {
                    _txtIngresosDetalles = value;
                }
                OnPropertyChanged(nameof(TxtIngresosDetalles));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OperacionesBancariasViewModel()
        {
            TxtIngresos = "Ingresos";
            TxtIngresosDetalles = "Detalles de Ingresos";
        }
    }


}
