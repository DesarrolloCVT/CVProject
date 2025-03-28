using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Egresos : ContentPage
{
    EgresosViewModel viewModel;

    public Egresos()
	{
        INavigationService navigationService = new NavigationService();
        ApiService apiService = new ApiService();

        InitializeComponent();
        BindingContext = new EgresosViewModel(navigationService, apiService);

        viewModel = BindingContext as EgresosViewModel;

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