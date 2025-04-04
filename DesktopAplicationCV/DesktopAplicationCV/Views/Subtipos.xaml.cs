using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModels;
using Syncfusion.Maui.Core.Carousel;

namespace DesktopAplicationCV.Views;

public partial class Subtipos : ContentPage
{
    SubtiposViewModel viewModel;

    public Subtipos()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new SubtiposViewModel(navigationService);

        viewModel = BindingContext as SubtiposViewModel;

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