using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>(string dato, object? obj = null, int? num = null) where TViewModel : BaseViewModel;
        void NavigationAsync(Page page);
        Task GoBackAsync();
    }
}
