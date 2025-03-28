using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Editar_Egresos : ContentPage
{
	public Editar_Egresos(object obj)
    {
        INavigationService navigationService = new NavigationService();
        ApiService apiService = new ApiService();

        InitializeComponent();
        BindingContext = new EgresosViewModel(navigationService, apiService);
        var viewModel = BindingContext as EgresosViewModel;

        EgresosModel egresosModel = (EgresosModel)obj;
        EditFolio.Text = egresosModel.Folio.ToString().Trim();
        EditTipo.SelectedItem = egresosModel.Tipo_Transaccion.Trim();
        EditSubTipo.SelectedItem = egresosModel.Subtipo_Transaccion.Trim();
        EditMoneda.SelectedItem = egresosModel.Moneda.Trim();
        PkrFecha.Date = egresosModel.Fecha;
        EditCliente.Text = egresosModel.Cliente.Trim();
        EditMetodoPago.SelectedItem = egresosModel.Metodo_Pago.Trim();
        EditBanco.Text = egresosModel.Banco.Trim();
        EditCuenta.Text = egresosModel.Cuenta.Trim();
    }
}