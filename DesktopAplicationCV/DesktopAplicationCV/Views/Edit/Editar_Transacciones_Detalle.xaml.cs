using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Editar_Transacciones_Detalle : ContentPage
{
	public Editar_Transacciones_Detalle(object obj)
	{
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();

        InitializeComponent();
        BindingContext = new TransaccionesDetalleViewModel(navigationService, auxService);
        var viewModel = BindingContext as TransaccionesDetalleViewModel;

        TransaccionesDetalleModel transaccionesDetalleModel = (TransaccionesDetalleModel)obj;

        EditIdTransaccion.Text = transaccionesDetalleModel.Id_Transaccion.ToString().Trim();
        EditFolioFactura.Text = transaccionesDetalleModel.Folio_Factura.ToString().Trim();
        EditTipoFactura.Text = transaccionesDetalleModel.Tipo_Factura.ToString().Trim();
        EditMonto.Text = transaccionesDetalleModel?.Monto.ToString().Trim();
    }
}