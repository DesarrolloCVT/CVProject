using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Banco_Detalle : ContentPage
{
	public Editar_Banco_Detalle(object obj)
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new BancoDetalleViewModel(navigationService);
        var viewModel = BindingContext as BancoDetalleViewModel;

        BancoDetalleModel bancoDetalleModel = (BancoDetalleModel)obj;
        EditCodigoBanco.Text = bancoDetalleModel.Codigo_Banco.ToString().Trim();
        EditNumero.Text = bancoDetalleModel.Numero.ToString().Trim();
        EditSaldo.Text = bancoDetalleModel.Saldo.ToString().Trim();
    }
}