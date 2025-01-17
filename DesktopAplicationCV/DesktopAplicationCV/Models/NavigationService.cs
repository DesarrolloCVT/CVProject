using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Models
{
    public class NavigationService : INavigationService
    {
        private readonly INavigation _navigation;

        // Constructor donde inyectamos el objeto INavigation (para navegar)
        public NavigationService(INavigation navigation)
        {
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
        }

        // Método para navegar a una página
        public async Task NavigateToAsync(string route)
        {
            var pageType = Type.GetType(route);
            if (pageType != null)
            {
                var page = (Page)Activator.CreateInstance(pageType);
                await _navigation.PushAsync(page);
            }
        }

        // Método para retroceder a la página anterior
        public async Task GoBackAsync()
        {
            await _navigation.PopAsync();
        }
    }
}
