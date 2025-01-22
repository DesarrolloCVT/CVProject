using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public class Productos
    {
        private int codigo;
        private string producto;

        private ICommand buttonCommand;

        public int Codigo
        {
            get { return this.codigo; }
            set { this.codigo = value; }
        }

        public string Producto
        {
            get { return producto; }
            set { this.producto = value; }
        }

        public Productos(int codigo, string producto)
        {
            this.Codigo = codigo;
            this.Producto = producto;
        }
    }
}
