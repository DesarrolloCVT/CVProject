using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DesktopAplicationCV.ViewModel
{
    public partial class EditorSocioViewModel : BaseViewModel
    {
        [ObservableProperty]
        public int codigo;

        [ObservableProperty]
        public string nombre;

        public override async Task InitializeAsync(object parameter)
        {
            if (parameter is not null)
            {
                var data = parameter as dynamic;
                Codigo = data?.Codigo ?? 0;
                Nombre = data?.Nombre ?? string.Empty;
            }

            // Simula una operación asincrónica (ejemplo: cargar más datos)
            await Task.Delay(500);
        }
    }
}
