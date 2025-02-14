using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Tipo : ContentPage
{
	public Editar_Tipo(object obj)
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new TipoViewModel(navigationService);
        var viewModel = BindingContext as TipoViewModel;

        TipoModel tipoModel = (TipoModel)obj;
        EditCodigo.Text = tipoModel.Codigo.ToString().Trim();
        EditNombre.Text = tipoModel.Nombre.Trim();
        PkrTipo.SelectedItem = tipoModel.Tipo_Dato.Trim();
        EditCuenta.Text = tipoModel.Cuenta.Trim();
        EditPagoFactura.Text = tipoModel.Pago_Factura.ToString().Trim();
        EditGastoComercializacion.Text = tipoModel.Gasto_Comercializacion.ToString().Trim();
        EditComisiones.Text = tipoModel.Comisiones.ToString().Trim();
        EditGastoFinanciero.Text = tipoModel.Gasto_Financiero.ToString().Trim();
        EditAnticipo.Text = tipoModel.Anticipo.ToString().Trim();
    }
}