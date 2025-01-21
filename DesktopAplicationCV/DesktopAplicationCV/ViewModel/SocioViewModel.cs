using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

using Syncfusion.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace DesktopAplicationCV.ViewModel
{
    public partial class SocioViewModel : BaseViewModel
    {
        #region Variables
        private readonly INavigationService _navigationService;

        public ICommand NavigateCommand { get; }

        [ObservableProperty]
        private string filterText;

        [ObservableProperty]
        private string codigo = "Codigo";

        [ObservableProperty]
        public string txtcodigo;

        [ObservableProperty]
        private string nombre = "Nombre";

        [ObservableProperty]
        public string txtnombre;

        [ObservableProperty]
        private string tipo = "Tipo";

        [ObservableProperty]
        public string txttipo;

        [ObservableProperty]
        private string saldo = "Saldo";

        [ObservableProperty]
        public string txtsaldo;

        [ObservableProperty]
        private int selectedIndex;

        [ObservableProperty]
        private ObservableCollection<OrderInfo> orderInfo;

        [ObservableProperty]
        private string seleccionado;

        #endregion

        public ObservableCollection<OrderInfo> Items { get; set; }
        public ICollectionView ItemsView { get; private set; }


        #region Inicializadores
        public ObservableCollection<OrderInfo> OrderInfoCollection
        {
            get { return orderInfo; }
            set { orderInfo = value; }
        }
        #endregion

        #region Constructores
        public SocioViewModel()
        {
            orderInfo = new ObservableCollection<OrderInfo>();
            GenerateOrders();
        }

        public SocioViewModel(INavigationService navigationService)
        {
            //_navigationService = navigationService;
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            // Inicializamos el comando de navegación
            NavigateCommand = new Command(OnNavigate);

            orderInfo = new ObservableCollection<OrderInfo>();
            GenerateOrders();
        }

        #endregion

        public void GenerateOrders()
        {
            orderInfo.Add(new OrderInfo(0,"Germany", "ALFKI", 10));
            orderInfo.Add(new OrderInfo(1,"Mexico", "ANATR", 10));
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
            orderInfo.Add(new OrderInfo(14,"CL", "TANGANANA", 1050));
            orderInfo.Add(new OrderInfo(15,"CL", "TANGANANICA", 3550));
        }

        #region Binding Methods 

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if(selectedIndex >= 0)
                {
                    OrderInfo.RemoveAt((SelectedIndex - 1));
                }
                else {
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
        public async Task Editar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    //Problema para la navegacion.
                    var parameter = new { Codigo = 123, Nombre = "Detalle de ejemplo" };
                    await _navigationService.NavigateToAsync("SocioNegocio");

                }else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Seleccione una fila para modificar", "Ok");
                }
            }catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
            }
        }
        [RelayCommand]
        public void Agregar()
        {
            Application.Current.MainPage.DisplayAlert("Alerta", "Selected.Rows - Count: ", "OK");
        }


        private void NombreEditor_Completed()
        {
            Seleccionado = "Nombre";
            if (!string.IsNullOrEmpty(Txtnombre))
            {
                //dataGrid.View.Filter = FilterRecords;
            }
            else
            {
                //dataGrid.View.Filter = null;
            }
            //dataGrid.View.RefreshFilter();
        }

        private void CodigoEditor_Completed()
        {
            Seleccionado = "Codigo";
            if (!string.IsNullOrEmpty(Txtcodigo))
            {
                //dataGrid.View.Filter = FilterRecords;
            }
            else
            {
                //dataGrid.View.Filter = null;
            }
            //dataGrid.View.RefreshFilter();
        }

        private void TipoEditor_Completed()
        {
            Seleccionado = "Tipo";
            if (!string.IsNullOrEmpty(Txttipo))
            {
                //dataGrid.View.Filter = FilterRecords;
            }
            else
            {
                //dataGrid.View.Filter = null;
            }
            //dataGrid.View.RefreshFilter();
        }

        private void SaldoEditor_Completed()
        {
            Seleccionado = "Saldo";
            if (!string.IsNullOrEmpty(Txtsaldo))
            {
                //dataGrid.View.Filter = FilterRecords;
            }
            else
            {
                //dataGrid.View.Filter = null;
            }
            //dataGrid.View.RefreshFilter();
        }

        private bool FilterRecords(object record)
        {
            var orderInfo = record as OrderInfo;
            switch (seleccionado)
            {
                case "Nombre":
                    if (orderInfo != null && orderInfo.Nombre.ToLower() == Txtnombre.ToLower())
                    {
                        return true;
                    }
                    return false;
                case "Codigo":
                    try
                    {
                        if (orderInfo != null && orderInfo.Codigo == int.Parse(Txtcodigo))
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                case "Tipo":
                    if (orderInfo != null && orderInfo.Tipo.ToLower() == Txttipo.ToLower())
                    {
                        return true;
                    }
                    return false;
                default:
                    try
                    {
                        if (orderInfo != null && orderInfo.Saldo == int.Parse(Txtsaldo))
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (Exception) 
                    {
                        return false;
                    }
            }
        }

        #endregion

        private async void OnNavigate()
        {
            // Usamos el servicio de navegación para navegar a otra página
            await _navigationService.NavigateToAsync("DesktopAplicationCV.YourPage");
        }

        /*
         * No posee Metodo, (Antiguo). 
         public override Task InitializeAsync(object parameter)
        {
            return base.InitializeAsync(parameter);
        }*/
    }
}
