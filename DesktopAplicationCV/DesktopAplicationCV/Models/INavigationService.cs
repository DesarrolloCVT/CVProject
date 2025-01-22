using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Models
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>(string dato) where TViewModel : BaseViewModel;
        Task GoBackAsync();
    }
}
