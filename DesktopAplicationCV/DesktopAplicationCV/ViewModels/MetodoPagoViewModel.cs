using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.Views;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Syncfusion.Maui.Data;
using Syncfusion.Maui.DataGrid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopAplicationCV.ViewModels
{
    public partial class MetodoPagoViewModel : BaseViewModel
    {
        #region Variables
        private static int IdMetodoPagoCeldaSeleccionada;
        private string NombreMetodoPagoCeldaSeleccionada;

        private static object _oldMethodPay;
        private object NewMethodPay;

        private readonly INavigationService _navigationService;
        private readonly MetodoPagoService _metodoPagoService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<MetodoPagoModel> MetodoPago;

        private string _filterText;
        private string _nombreMetodoPagoIngresadoText;

        private string _editNombreMetodoPago;

        public ICommand CargarMetodoPagoCommand { get; }
        public ICommand AgregarMetodoPagoCommand { get; }
        public ICommand EliminarMetodoPagoCommand { get; }
        public ICommand ActualizarMetodoPagoCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Encapsulado
        public ObservableCollection<MetodoPagoModel> MetodoPagoInfoCollection
        {
            get { return MetodoPago; }
            set { MetodoPago = value; }
        }

        // Propiedad para enlazar el texto del filtro desde la vista
        public string FilterText
        {
            get => _filterText;
            set
            {
                if (_filterText != value)
                {
                    _filterText = value;
                    OnPropertyChanged(nameof(FilterText));
                    // Llamar a la acción para actualizar el filtro
                    ApplyFilterAction?.Invoke();
                }
            }
        }

        public object OldMethodPay
        {
            get => _oldMethodPay;
            set
            {
                if (_oldMethodPay != value)
                {
                    _oldMethodPay = value;
                }
            }
        }

        public string NombreMetodoPagoIngresado
        {
            get => _nombreMetodoPagoIngresadoText;
            set
            {
                if (_nombreMetodoPagoIngresadoText != value)
                {
                    _nombreMetodoPagoIngresadoText = value;
                    OnPropertyChanged(nameof(NombreMetodoPagoIngresado));
                }
            }
        }

        public string EditNombreMetodoPago
        {
            get => _editNombreMetodoPago;
            set
            {
                if (_editNombreMetodoPago != value)
                {
                    _editNombreMetodoPago = value;
                    OnPropertyChanged(nameof(EditNombreMetodoPago));
                }
            }
        }
        // Acción para establecer la lógica del filtro
        public Action ApplyFilterAction { get; set; }
        #endregion

        #region Constructores
        public MetodoPagoViewModel(INavigationService navigationService)
        {
            _metodoPagoService = new MetodoPagoService();
            MetodoPago = new ObservableCollection<MetodoPagoModel>();
            _navigationService = navigationService;
            
            CargarMetodoPago();

            CeldaTocadaCommand = new Command<DataGridCellTappedEventArgs>(CeldaTocada);
        }
        #endregion

        #region Metodos 
        [RelayCommand]
        public void Cancelar()
        {
            _navigationService.GoBackAsync();
        }

        // Método que se ejecuta cuando se toca una celda
        private void CeldaTocada(DataGridCellTappedEventArgs e)
        {
            try
            {
                if (e.RowData is MetodoPagoModel metodoPago)
                {
                    IdMetodoPagoCeldaSeleccionada = metodoPago.Id_Metodo_Pago;
                    NombreMetodoPagoCeldaSeleccionada = metodoPago.Nombre;
                    // Aquí puedes manejar la lógica de negocio sin tocar la vista
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error CeldaTocada MetodoPagoViewModel: " + ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [RelayCommand]
        public void GridCargado()
        {
            CargarMetodoPago();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    EliminarMetodoPago(IdMetodoPagoCeldaSeleccionada);
                    CargarMetodoPago();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
                Console.WriteLine("Error Eliminar MetodoPagoViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void Agregar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Metodo_Pago");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                Console.WriteLine("Error Agregar MetodoPagoViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void InsertarMetodoPago()
        {
            try
            {
                if (!string.IsNullOrEmpty(NombreMetodoPagoIngresado))
                {
                    AgregarMetodoPago(new MetodoPagoModel(0,NombreMetodoPagoIngresado));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error InsertarBanco MetodoPagoViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        private async void Editar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    try
                    {
                        OldMethodPay = new MetodoPagoModel(IdMetodoPagoCeldaSeleccionada, NombreMetodoPagoCeldaSeleccionada)
                        {
                            Nombre = NombreMetodoPagoCeldaSeleccionada
                        };

                        await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Metodo_Pago", OldMethodPay);
                    }
                    catch (Exception Ex)
                    {
                        Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                    }
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Debe seleccionar una fila valida", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Editar MetodoPagoViewModel: " + Ex.Message);
            }
        }

        private async Task CargarMetodoPago()
        {
            try
            {
                var metodoPagos = await _metodoPagoService.GetMetodoPagoAsync();
                MetodoPago.Clear();
                foreach (var metodoPago in metodoPagos)
                {
                    MetodoPago.Add(metodoPago);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Editar MetodoPagoViewModel: " + Ex.Message);
            }
        }

        private async Task AgregarMetodoPago(MetodoPagoModel metodoPago)
        {
            try
            {
                if (await _metodoPagoService.AddMetodoPagoAsync(metodoPago))
                {
                    MetodoPago.Add(metodoPago);
                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente. ", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error, ya existe una entrada asignada con este Codigo.", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error AgregarMetodoPago MetodoPagoViewModel: " + Ex.Message);
            }
        }

        private async Task EliminarMetodoPago(int id)
        {
            try
            {
                if (await _metodoPagoService.DeleteMetodoPagoAsync(id))
                {
                    var metodoPago = MetodoPago.FirstOrDefault(p => p.Id_Metodo_Pago == id);
                    if (metodoPago != null)
                    {
                        MetodoPago.Remove(metodoPago);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error EliminarMetodoPago MetodoPagoViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public void Update()
        {
            try
            {
                ActualizarMetodoPago((MetodoPagoModel)OldMethodPay);
                Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");
                _navigationService.GoBackAsync();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Update MetodoPagoViewModel: " + Ex.Message);
            }
        }

        private async Task ActualizarMetodoPago(MetodoPagoModel AntiguoMetodoPago)
        {
            try
            {
                Console.WriteLine("EditNombreMetodoPago: " + EditNombreMetodoPago);

                var id = IdMetodoPagoCeldaSeleccionada;
                var name = EditNombreMetodoPago;

                NewMethodPay = new MetodoPagoModel(id, name)
                {
                    Id_Metodo_Pago = id,
                    Nombre = name
                };

                if (await _metodoPagoService.UpdateMetodoPagoAsync((MetodoPagoModel)NewMethodPay))
                {
                    //Remove Old Product
                    MetodoPago.Remove(AntiguoMetodoPago);

                    //Add new product
                    MetodoPago.Add((MetodoPagoModel)NewMethodPay);

                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error ActualizarBanco BancoViewModel: " + Ex.Message);
            }
        }

        // Lógica de filtrado como delegado
        public Predicate<object> GetFilter()
        {
            return item =>
            {
                if (item is MetodoPagoModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Id_Metodo_Pago.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Nombre.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }
        #endregion
    }
}
