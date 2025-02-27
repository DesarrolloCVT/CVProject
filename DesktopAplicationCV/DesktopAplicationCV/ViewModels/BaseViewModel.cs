using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModels;
using DesktopAplicationCV.Views;

namespace DesktopAplicationCV.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        public virtual Task InitializeAsync(object parameter)
        {
            return Task.CompletedTask;
        }
    }
}
