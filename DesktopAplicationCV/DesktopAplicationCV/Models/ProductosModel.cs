using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public class ProductosModel
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

        public ProductosModel(int codigo, string producto)
        {
            this.Codigo = codigo;
            this.Producto = producto;
        }
    }
}
