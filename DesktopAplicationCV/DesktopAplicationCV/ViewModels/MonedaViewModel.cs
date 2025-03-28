using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.ViewModels
{
    public partial class MonedaViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<string> monedas;

        [ObservableProperty]
        private string monedaSeleccionada;

        public MonedaViewModel() => Monedas = new ObservableCollection<string> { "USD", "EUR", "CLP" };
    }
}
