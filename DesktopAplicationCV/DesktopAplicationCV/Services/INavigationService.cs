using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>(string dato, object obj = null) where TViewModel : BaseViewModel;
        Task GoBackAsync();
    }
}
