using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class Editar_Socio_Negocio : ContentPage
{
	public Editar_Socio_Negocio(object obj)
	{
        INavigationService navigationService = new NavigationService();
        AuxService auxService = new AuxService();

        InitializeComponent();
        BindingContext = new SocioViewModel(navigationService, auxService);
        var viewModel = BindingContext as SocioViewModel;

        SocioNegocioModel socioModel = (SocioNegocioModel)obj;
        EditCodigo.Text = socioModel.Codigo.ToString().Trim();
        EditNombre.Text = socioModel.Nombre.Trim();
    }
}