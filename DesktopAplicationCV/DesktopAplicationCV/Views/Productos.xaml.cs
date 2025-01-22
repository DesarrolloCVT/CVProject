using DesktopAplicationCV.Models;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Productos : ContentPage
{
	public Productos()
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        try
        {
            BindingContext = new ProductosViewModel(navigationService);

            var viewModel = BindingContext as ProductosViewModel;

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
        catch (Exception ex)
        {
            Console.WriteLine("Exception: Socio_Negocio: " + ex.Message);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}