using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Views;
using Syncfusion.Maui.Core.Carousel;
using Syncfusion.Maui.DataGrid;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace DesktopAplicationCV.Models
{
    public partial class SocioViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private int selectedIndex;

        [ObservableProperty]
        private ObservableCollection<OrderInfo> orderInfo;

        [ObservableProperty]
        private string seleccionado;

        [ObservableProperty]
        private int codigo;

        [ObservableProperty]
        private string nombre;

        public ObservableCollection<OrderInfo> OrderInfoCollection
        {
            get { return orderInfo; }
            set { orderInfo = value; }
        }

        public SocioViewModel()//INavigationService navigationService)
        {
            //_navigationService = navigationService;
            orderInfo = new ObservableCollection<OrderInfo>();
            GenerateOrders();
        }

        public SocioViewModel(INavigationService navigationService)//INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void GenerateOrders()
        {
            orderInfo.Add(new OrderInfo(0, "Germany", "ALFKI", 10));
            orderInfo.Add(new OrderInfo(1, "Mexico", "ANATR", 10));
            orderInfo.Add(new OrderInfo(2,"Mexico", "ANTON", 10));
            orderInfo.Add(new OrderInfo(3,"UK", "AROUT", 10));
            orderInfo.Add(new OrderInfo(4,"Sweden", "BERGS", 10));
            orderInfo.Add(new OrderInfo(5,"Germany", "BLAUS", 10));
            orderInfo.Add(new OrderInfo(6,"France", "BLONP", 10));
            orderInfo.Add(new OrderInfo(7,"Spain", "BOLID", 10));
            orderInfo.Add(new OrderInfo(8,"France", "BONAP", 10));
            orderInfo.Add(new OrderInfo(9,"Canada", "BOTTM", 10));
            orderInfo.Add(new OrderInfo(10,"UK", "AROUT", 10));
            orderInfo.Add(new OrderInfo(11,"Germany", "BLAUS", 10));
            orderInfo.Add(new OrderInfo(12,"France", "BLONP", 10));
            orderInfo.Add(new OrderInfo(13,"UK", "AROUT", 10));
            orderInfo.Add(new OrderInfo(14, "CL", "TANGANANA", 1050));
            orderInfo.Add(new OrderInfo(15, "CL", "TANGANANICA", 3550));
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if(selectedIndex >= 0)
                {
                    OrderInfo.RemoveAt((SelectedIndex - 1));
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
                #region Codigo original
                /*if (SelectedRows.Count > 0)
                {
                    foreach (var item in SelectedRows)
                    {
                        OrderInfo.Remove((OrderInfo)item);
                    }
                    this.dataGrid!.ClearSelection();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Seleccione las filas que desea eliminar", "Ok");
                }*/
                #endregion
            }
            catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
            }
        }
        [RelayCommand]
        public void Editar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    //Problema para la navegacion.
                    _navigationService.NavigateToAsync("Editar_Socio_Negocio");
                    //navigation.PushAsync(new Editar_Socio_Negocio(0, "Germany"));
                    //Navigation?.PushAsync(new Editar_Socio_Negocio(0, "Germany"));
                    
                    //Navigation.PushAsync(new Editar_Socio_Negocio(0, "Germany"));
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Seleccione una fila para modificar", "Ok");
                }
            }
            catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
            }
        }

        [RelayCommand]
        public void Agregar()
        {
            Application.Current.MainPage.DisplayAlert("Alerta", "Selected.Rows - Count: ", "OK");
        }

        private async void OnNavigate()
        {
            await _navigationService.NavigateToAsync("Namespace.PageName");
        }
    }
}
