using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Ingresos : ContentPage
{
    IngresosViewModel viewModel;
    public Ingresos()
	{
        INavigationService navigationService = new NavigationService();
        ApiService apiService = new ApiService();

        InitializeComponent();
        BindingContext = new IngresosViewModel(navigationService, apiService);

        viewModel = BindingContext as IngresosViewModel;

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

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}