using DesktopAplicationCV.Models;
using System.Collections.ObjectModel;

namespace DesktopAplicationCV.ViewModel
{
    public interface IUsuarioViewModel
    {
        ObservableCollection<Usuario> Usuarios { get; set; }

        Task CargarUsuariosAsync();
    }
}