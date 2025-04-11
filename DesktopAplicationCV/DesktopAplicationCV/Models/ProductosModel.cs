using System.Windows.Input;

namespace DesktopAplicationCV.Models
{
    public class ProductosModel
    {
        private int id_producto;
        private string codigo;
        private string producto;

        private ICommand buttonCommand;

        public int Id_Producto
        {
            get { return this.id_producto; }
            set { this.id_producto = value; }
        }

        public string Codigo
        {
            get { return this.codigo; }
            set { this.codigo = value; }
        }

        public string Producto
        {
            get { return producto; }
            set { this.producto = value; }
        }

        public ProductosModel(int id_producto, string codigo, string producto)
        {
            this.Id_Producto = id_producto;
            this.Codigo = codigo;
            this.Producto = producto;
        }
    }
}
