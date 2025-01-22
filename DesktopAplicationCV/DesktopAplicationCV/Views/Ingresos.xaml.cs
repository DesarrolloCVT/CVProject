using DesktopAplicationCV.Models;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Ingresos : ContentPage
{
	public Ingresos()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new IngresosViewModel(navigationService);

        var viewModel = BindingContext as SocioViewModel;

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