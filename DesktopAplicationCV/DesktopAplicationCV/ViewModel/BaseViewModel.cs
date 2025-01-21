using CommunityToolkit.Mvvm.ComponentModel;

namespace DesktopAplicationCV.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private string agregar = "Agregar";

        [ObservableProperty]
        private string eliminar = "Eliminar";

        [ObservableProperty]
        private string editar = "Editar";

        public virtual Task InitializeAsync(object parameter)
        {
            return Task.CompletedTask;
        }
    }
}
