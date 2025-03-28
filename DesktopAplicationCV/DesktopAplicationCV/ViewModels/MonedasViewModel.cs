using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.ViewModels
{
    public class MonedasViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private List<MonedaModel> _monedas;
        private MonedaModel _monedaSeleccionada;

        public List<MonedaModel> Monedas
        {
            get => _monedas;
            set
            {
                _monedas = value;
                OnPropertyChanged();
            }
        }

        public MonedaModel MonedaSeleccionada
        {
            get => _monedaSeleccionada;
            set
            {
                _monedaSeleccionada = value;
                OnPropertyChanged();
            }
        }

        public MonedasViewModel()
        {
            _apiService = new ApiService();
            //CargarMonedas();
        }

        /*private async void CargarMonedas()
        {
            Monedas = await _apiService.GetMonedasAsync();
        }*/

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
