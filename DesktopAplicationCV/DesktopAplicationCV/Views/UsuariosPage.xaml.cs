using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class UsuariosPage : ContentPage
{
    private UsuarioViewModel _viewModel;

    public UsuariosPage(UsuarioViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.CargarUsuariosAsync();
    }
}