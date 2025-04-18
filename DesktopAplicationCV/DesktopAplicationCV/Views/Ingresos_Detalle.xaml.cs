using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Ingresos_Detalle : ContentPage
{
	public Ingresos_Detalle(int IdValido)
	{
        Console.WriteLine("Id: " + IdValido);
        INavigationService navigationService = new NavigationService();
        InitializeComponent();
        BindingContext = new IngresoDetalleViewsModel(navigationService);

        var viewModel = BindingContext as IngresoDetalleViewsModel;
        viewModel.Id_Ingreso = IdValido;

        if (viewModel != null)
        {
            // Vincular la acci�n para aplicar el filtro
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