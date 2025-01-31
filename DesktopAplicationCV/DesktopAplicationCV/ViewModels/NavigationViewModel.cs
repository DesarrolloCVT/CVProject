using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Services;
using System.Windows.Input;

namespace DesktopAplicationCV.ViewModel
{
    public  class NavigationViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public NavigationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand GoBackCommand => new AsyncRelayCommand(GoBack);

        private async Task GoBack()
        {
            await _navigationService.GoBackAsync();
        }
    }
}

