using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Login : ContentPage
{
    public Login(LoginViewModel loginView)
	{
        InitializeComponent();

        BindingContext = loginView;
        //var viewModel = BindingContext as LoginViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as LoginViewModel;
        if (viewModel != null) 
        {
            viewModel.CleanLogin();
        }
    }
}