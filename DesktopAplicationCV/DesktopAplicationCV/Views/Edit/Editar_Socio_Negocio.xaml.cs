using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Socio_Negocio : ContentPage
{
	public Editar_Socio_Negocio(object obj)
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new SocioViewModel(navigationService);
        var viewModel = BindingContext as SocioViewModel;

        SocioNegocioModel socioModel = (SocioNegocioModel)obj;
        EditCodigo.Text = socioModel.Codigo.ToString().Trim();
        EditNombre.Text = socioModel.Nombre.Trim();
    }
}