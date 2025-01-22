namespace DesktopAplicationCV.Views;

using DesktopAplicationCV.Models;
using DesktopAplicationCV.ViewModel;

public partial class Productos : ContentPage
{
	public Productos()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new ProductosViewModel(navigationService);

        var viewModel = BindingContext as ProductosViewModel;

        if (viewModel != null)
        {
            // Vincular la acci�n para aplicar el filtro
            viewModel.ApplyFilterAction = () =>
            {
                dataGridProducto.View.Filter = viewModel.GetFilter();
                dataGridProducto.View.RefreshFilter();
            };
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}