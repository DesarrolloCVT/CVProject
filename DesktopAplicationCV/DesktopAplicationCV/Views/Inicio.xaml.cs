using DesktopAplicationCV.ViewModels;

namespace DesktopAplicationCV.Views;

public partial class Inicio : ContentPage
{
	public Inicio(LoginViewModel loginViewModel)
	{
		InitializeComponent();
        BindingContext = loginViewModel;
    }
}