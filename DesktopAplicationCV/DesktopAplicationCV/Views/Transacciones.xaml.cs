using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.ViewModels;
using Syncfusion.Maui.Core.Carousel;

namespace DesktopAplicationCV.Views;

public partial class Transacciones : ContentPage
{
    TransaccionesViewModel viewModel;
    public Transacciones()
	{
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();

        InitializeComponent();
        BindingContext = new TransaccionesViewModel(navigationService, auxService);

        viewModel = BindingContext as TransaccionesViewModel;

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