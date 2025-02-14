using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Banco : ContentPage
{
	public Editar_Banco(object obj)
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new BancoViewModel(navigationService);
        var viewModel = BindingContext as BancoViewModel;

        BancoModel bancoModel = (BancoModel)obj;
        EditCodigo.Text = bancoModel.Codigo.ToString().Trim();
        EditNombre.Text = bancoModel.Nombre.Trim();
    }
}