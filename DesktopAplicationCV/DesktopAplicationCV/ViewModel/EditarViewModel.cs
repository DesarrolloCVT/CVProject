using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace DesktopAplicationCV.ViewModel
{
    public partial class EditarViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public EditarViewModel(INavigationService navigationService)
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
