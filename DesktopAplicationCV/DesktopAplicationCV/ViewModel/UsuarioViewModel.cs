using DesktopAplicationCV.Data;
using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopAplicationCV.ViewModel
{
    public partial class UsuarioViewModel : BaseViewModel
    {
        private readonly UsuarioService _usuarioService;
        public ObservableCollection<Usuario> Usuarios { get; set; }
        public ICommand CargarUsuariosCommand { get; }

        public UsuarioViewModel(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
            Usuarios = new ObservableCollection<Usuario>();
            CargarUsuariosCommand = new Command(async () => await CargarUsuariosAsync());
        }

        public async Task CargarUsuariosAsync()
        {
            var usuarios = await _usuarioService.ObtenerUsuariosAsync();
            Usuarios.Clear();

            foreach (var usuario in usuarios)
            {
                Usuarios.Add(usuario);
            }
        }
    }
}
