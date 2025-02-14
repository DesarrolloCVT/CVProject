using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Ingresos_Detalle : ContentPage
{
	public Editar_Ingresos_Detalle(object obj)
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new IngresoDetalleViewsModel(navigationService);
        var viewModel = BindingContext as IngresoDetalleViewsModel;

        IngresoDetalleModel ingresoDetalleModel = (IngresoDetalleModel)obj;
        EditFolio.Text = ingresoDetalleModel.Folio_FacturaVenta.ToString().Trim();
        EditMonto.Text = ingresoDetalleModel.Monto.ToString().Trim();
    }
}