using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModels;
using DesktopAplicationCV.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Services.Maps;

namespace DesktopAplicationCV.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        public BaseViewModel()
        {
        }

        public virtual Task InitializeAsync(object parameter)
        {
            return Task.CompletedTask;
        }
    }
}
