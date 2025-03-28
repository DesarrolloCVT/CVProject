using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Ingresos : ContentPage
{
	public Editar_Ingresos(object obj)
	{
        INavigationService navigationService = new NavigationService();
        ApiService apiService = new ApiService();

        InitializeComponent();
        BindingContext = new IngresosViewModel(navigationService, apiService);
        var viewModel = BindingContext as IngresosViewModel;

        IngresosModel ingresosModel = (IngresosModel)obj;
        EditFolio.Text = ingresosModel.Folio.ToString().Trim();
        EditTipo.Text = ingresosModel.Tipo_Transaccion.Trim();
        EditSubTipo.SelectedItem = ingresosModel.Subtipo_Transaccion.Trim();
        EditMoneda.SelectedItem = ingresosModel.Moneda.Trim();
        PkrFecha.Date = ingresosModel.Fecha;
        EditCliente.Text = ingresosModel.Cliente.Trim();
        EditMetodoPago.SelectedItem = ingresosModel.Metodo_Pago.Trim();
        EditBanco.Text = ingresosModel.Banco.Trim();
        EditCuenta.Text = ingresosModel.Cuenta.Trim();
    }
}