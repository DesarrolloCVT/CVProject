using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Monedas : ContentPage
{
    MonedasViewModel viewModel;

    public Monedas()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new MonedasViewModel(navigationService);

        viewModel = BindingContext as MonedasViewModel;

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