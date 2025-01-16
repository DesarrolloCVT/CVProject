using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.Views;



namespace DesktopAplicationCV.Models
{
    public class NavigationService : INavigationService
    {
        private readonly IDictionary<string, Type> _pages = new Dictionary<string, Type>();
        private NavigationPage _navigationPage;

        public void Configure(string pageKey, Type pageType)
        {
            if (_pages.ContainsKey(pageKey))
            {
                throw new ArgumentException($"La clave '{pageKey}' ya está registrada.");
            }
            _pages[pageKey] = pageType;
        }

        public void SetNavigationPage(NavigationPage navigationPage)
        {
            _navigationPage = navigationPage ?? throw new ArgumentNullException(nameof(navigationPage));
        }

        public async Task NavigateToAsync(string pageKey, object parameter = null)
        {
            if (!_pages.ContainsKey(pageKey))
            {
                throw new ArgumentException($"No se encontró una página registrada con la clave '{pageKey}'.");
            }

            var pageType = _pages[pageKey];
            var page = Activator.CreateInstance(pageType) as Page;

            if (page == null)
            {
                throw new InvalidOperationException($"No se pudo crear una instancia de la página '{pageKey}'.");
            }

            // Pasar el parámetro al BindingContext del ViewModel si es necesario
            if (parameter != null && page.BindingContext is BaseViewModel viewModel)
            {
                await viewModel.InitializeAsync(parameter);
            }

            if (_navigationPage != null)
            {
                await _navigationPage.PushAsync(page);
            }
            else
            {
                throw new InvalidOperationException("NavigationPage no está inicializado. Asegúrate de configurarlo antes de navegar.");
            }
        }

        public async Task GoBackAsync()
        {
            if (_navigationPage != null)
            {
                await _navigationPage.PopAsync();
            }
            else
            {
                throw new InvalidOperationException("NavigationPage no está inicializado. Asegúrate de configurarlo antes de navegar.");
            }
        }
    }
}
