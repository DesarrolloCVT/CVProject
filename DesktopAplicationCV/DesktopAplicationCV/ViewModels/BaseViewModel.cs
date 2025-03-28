using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModels;
using DesktopAplicationCV.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Services.Maps;

namespace DesktopAplicationCV.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        /*[ObservableProperty]
        private ObservableCollection<string> monedas;*/

        [ObservableProperty]
        private ObservableCollection<string> tipos;

        [ObservableProperty]
        private ObservableCollection<string> motivoIngreso;

        [ObservableProperty]
        private ObservableCollection<string> motivoEgreso;

        [ObservableProperty]
        private ObservableCollection<string> metodoPago;

        public BaseViewModel()
        {
            Tipos = new ObservableCollection<string> { "Ingreso", "Egreso" };
            //Monedas = new ObservableCollection<string> { "USD", "EUR", "CLP" };
            MotivoIngreso = new ObservableCollection<string> { "Pago de Factura" , "Devolucion de Dinero" };
            MotivoEgreso = new ObservableCollection<string> { "Pago de Factura" , "Gasto de Comercializacion", "Comisiones" , "Gasto Financiero" , "Anticipo" };
            MetodoPago = new ObservableCollection<string> { "Transferencia" };

        }

        public virtual Task InitializeAsync(object parameter)
        {
            return Task.CompletedTask;
        }
    }
}
