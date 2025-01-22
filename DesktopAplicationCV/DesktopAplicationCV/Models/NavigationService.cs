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

        private Type GetPageTypeForViewModel(Type viewModelType, string id)
        {
            var viewName = viewModelType.FullName.Replace("ViewModel", "Views");
            var viewNameValidate = (viewName + id);
            var viewType = Type.GetType(viewNameValidate);

            if (viewType == null)
            {
                throw new InvalidOperationException($"No se pudo localizar la página para {viewModelType}");
            }

            return viewType;
        }
    }
}
