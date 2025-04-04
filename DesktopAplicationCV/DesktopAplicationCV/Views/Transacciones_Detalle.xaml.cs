using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModels;
using Syncfusion.Maui.Core.Carousel;

namespace DesktopAplicationCV.Views;

public partial class Transacciones_Detalle : ContentPage
{
    TransaccionesDetalleViewModel viewModel;
    public Transacciones_Detalle(int IdValido)
	{
        Console.WriteLine("Id: " + IdValido);
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();

        InitializeComponent();
        BindingContext = new TransaccionesDetalleViewModel(navigationService, auxService);

        viewModel = BindingContext as TransaccionesDetalleViewModel;
        viewModel.Id_Transaccion = IdValido;

        if (viewModel != null)
        {
            // Vincular la acción para aplicar el filtro
            viewModel.ApplyFilterAction = () =>
            {
                dataGrid.View.Filter = viewModel.GetFilter();
                dataGrid.View.RefreshFilter();
            };
        }
    }
}