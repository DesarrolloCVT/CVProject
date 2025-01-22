namespace DesktopAplicationCV.Views;

using DesktopAplicationCV.Models;
using DesktopAplicationCV.ViewModel;

public partial class Socio_Negocio : ContentPage
{
    public Socio_Negocio()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new SocioViewModel(navigationService);

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