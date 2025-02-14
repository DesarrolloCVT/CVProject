using DesktopAplicationCV.Services;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.ViewModel;
using System.Reflection;

namespace DesktopAplicationCV.Views;

public partial class Editar_Productos : ContentPage
{
	public Editar_Productos(object obj)
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new ProductosViewModel(navigationService);
        var viewModel = BindingContext as ProductosViewModel;

        ProductosModel productoModel = (ProductosModel)obj;
        EditCodigo.Text = productoModel.Codigo.ToString().Trim();
        EditProducto.Text = productoModel.Producto.Trim();
    }
}