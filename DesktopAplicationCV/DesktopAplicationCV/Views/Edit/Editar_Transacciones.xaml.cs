using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Editar_Transacciones : ContentPage
{
	public Editar_Transacciones(object obj)
	{
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();

        InitializeComponent();
        BindingContext = new TransaccionesViewModel(navigationService, auxService);
        var viewModel = BindingContext as TransaccionesViewModel;

        TransaccionesModel transaccionesModel = (TransaccionesModel)obj;
        EditFolio.Text = transaccionesModel?.Folio.ToString().Trim();
        PkrTipo.SelectedItem = transaccionesModel.Tipo_Transaccion.Trim();
        PkrSubTipo.SelectedItem = transaccionesModel.Subtipo_Transaccion.Trim();
        EditMoneda.Text = transaccionesModel?.Moneda.Trim();
        PkrFecha.Date = transaccionesModel.Fecha;
        EditCliente.Text = transaccionesModel?.Cliente.ToString().Trim();
        EditMetodoPago.Text = transaccionesModel?.Metodo_Pago.ToString().Trim();
        EditBanco.Text = transaccionesModel?.Banco.ToString().Trim();
        EditCuenta.Text = transaccionesModel?.Cuenta.ToString().Trim();
    }
}