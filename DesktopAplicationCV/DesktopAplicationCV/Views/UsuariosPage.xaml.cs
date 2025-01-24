using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Views;

public partial class UsuariosPage : ContentPage
{
	public UsuariosPage()
	{   
        InitializeComponent();
		try
		{
            //BindingContext = App.Current.Handler?.MauiContext?.Services?.GetService<UsuarioViewModel>();
            HandlerChanged += OnHandlerChanged;
        }
		catch (Exception ex)
		{
			Console.WriteLine("Error: " + ex.Message);
		}
        
    }

    void OnHandlerChanged(object sender, EventArgs e)
    {
        BindingContext = Handler.MauiContext.Services.GetService<UsuarioViewModel>();
    }
}