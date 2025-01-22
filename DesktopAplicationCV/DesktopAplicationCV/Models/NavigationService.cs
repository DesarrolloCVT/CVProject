using DesktopAplicationCV.ViewModel;

namespace DesktopAplicationCV.Models
{   
    public class NavigationService : INavigationService
    {
        public async Task NavigateToAsync<TViewModel>(string identificador) where TViewModel : BaseViewModel
        {

            var pageType = GetPageTypeForViewModel(typeof(TViewModel), identificador);
            var page = (Page)Activator.CreateInstance(pageType);
            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                await navigationPage.Navigation.PushAsync(page);
            }
        }

        public async Task GoBackAsync()
        {
            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                await navigationPage.Navigation.PopAsync();
            }
        }

        private Type GetPageTypeForViewModel(Type viewModelType, string Nombre)
        {
            var viewOrigin = viewModelType.FullName.Replace("ViewModel", "Views");
            var viewName = viewOrigin.Replace("NavigationViews", Nombre);
            var viewType = Type.GetType(viewName);

            if (viewType == null)
            {
                throw new InvalidOperationException($"No se pudo localizar la página para {viewModelType}");
            }

            return viewType;
        }
    }
}
