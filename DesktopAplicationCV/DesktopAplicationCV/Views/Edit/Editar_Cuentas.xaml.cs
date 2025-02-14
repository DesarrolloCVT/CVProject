using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Cuentas : ContentPage
{
	public Editar_Cuentas(object obj)
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new CuentasViewModel(navigationService);
        var viewModel = BindingContext as CuentasViewModel;

        CuentasModel cuentasModel = (CuentasModel)obj;
        EditCodigo.Text = cuentasModel.Codigo.ToString().Trim();
        EditNombre.Text = cuentasModel.Nombre.Trim();
    }
}