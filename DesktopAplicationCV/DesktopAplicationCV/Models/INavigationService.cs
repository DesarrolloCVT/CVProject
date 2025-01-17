namespace DesktopAplicationCV.Models
{
    public interface INavigationService
    {
        Task NavigateToAsync(string route);
        Task GoBackAsync();
    }
}
