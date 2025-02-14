using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Ingresos : ContentPage
{
	public Editar_Ingresos(object obj)
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new IngresosViewModel(navigationService);
        var viewModel = BindingContext as IngresosViewModel;

        IngresosModel ingresosModel = (IngresosModel)obj;
        EditFolio.Text = ingresosModel.Folio.ToString().Trim();
        EditTipo.Text = ingresosModel.Tipo.Trim();
        EditMoneda.Text = ingresosModel.Moneda.Trim();
        PkrFecha.Date = ingresosModel.Fecha;
        EditCliente.Text = ingresosModel.Cliente.Trim();
        EditMetodoPago.Text = ingresosModel.Metodo_Pago.Trim();
        EditBanco.Text = ingresosModel.Banco.Trim();
        EditCuenta.Text = ingresosModel.Cuenta.Trim();
    }
}