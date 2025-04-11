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
        PkrMoneda.SelectedItem = transaccionesModel?.Moneda.Trim();
        PkrFecha.Date = transaccionesModel.Fecha;
        PkrCliente.SelectedItem = transaccionesModel?.Cliente.ToString().Trim();
        PkrMetodoPago.SelectedItem = transaccionesModel?.Metodo_Pago.ToString().Trim();
        PkrBanco.SelectedItem = transaccionesModel?.Banco.ToString().Trim();
        PkrCuenta.SelectedItem = transaccionesModel?.Cuenta.ToString().Trim();
    }
}