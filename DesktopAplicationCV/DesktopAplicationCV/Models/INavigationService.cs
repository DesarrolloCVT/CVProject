namespace DesktopAplicationCV.Models
{
    public interface INavigationService
    {
        Task NavigateToAsync(string pageKey, object parameter = null);
        Task GoBackAsync();
        void Configure(string pageKey, Type pageType);
    }
}
