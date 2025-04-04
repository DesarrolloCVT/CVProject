using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModels;
using Syncfusion.Maui.Core.Carousel;

namespace DesktopAplicationCV.Views;

public partial class Metodo_Pago : ContentPage
{
    MetodoPagoViewModel viewModel;
    public Metodo_Pago()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new MetodoPagoViewModel(navigationService);

        viewModel = BindingContext as MetodoPagoViewModel;

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