
namespace DesktopAplicationCV.Models
{
    public class NavigationService : INavigationService
    {
        private readonly INavigation _navigation;

        public NavigationService(INavigation navigation)
        {
            _navigation = navigation;
        }

        public async Task NavigateToAsync(string route)
        {
            var page = Activator.CreateInstance(Type.GetType(route));
            if (page != null)
            {
                await _navigation.PushAsync((Page)page);
            }
        }

        public async Task GoBackAsync()
        {
            await _navigation.PopAsync();
        }
    }
}
