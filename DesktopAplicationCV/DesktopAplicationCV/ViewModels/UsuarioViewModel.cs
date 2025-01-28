using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DesktopAplicationCV.ViewModel
{
    public class UsuarioViewModel
    {
        private readonly UsuarioService _usuarioService;

        public ObservableCollection<Usuario> Usuarios { get; set; }

        public UsuarioViewModel(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
            Usuarios = new ObservableCollection<Usuario>();
        }

        public async Task CargarUsuariosAsync()
        {
            try
            {
                var usuarios = await _usuarioService.ObtenerUsuariosAsync();
                Usuarios.Clear();
                foreach (var usuario in usuarios)
                {
                    Usuarios.Add(usuario);
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task AgregarUsuarioAsync(Usuario usuario)
        {
            await _usuarioService.AgregarUsuarioAsync(usuario);
            Usuarios.Add(usuario);
        }
    }
}