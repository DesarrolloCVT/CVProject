using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Editar_Subtipos : ContentPage
{
	public Editar_Subtipos(object obj)
	{
        INavigationService navigationService = new NavigationService();

        InitializeComponent();
        BindingContext = new SubtiposViewModel(navigationService);
        var viewModel = BindingContext as SubtiposViewModel;

        SubtiposModel subTipoModel = (SubtiposModel)obj;
        PkrIdentificador.SelectedItem = subTipoModel.Identificador.Trim();
        EditNombre.Text = subTipoModel.Nombre.Trim();
    }
}