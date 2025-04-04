using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
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
    public partial class MonedasViewModel : BaseViewModel
    {
        #region Variables
        private static int IdMonedaCeldaSeleccionada;
        private string NombreMonedaCeldaSeleccionada;

        private static object _oldCurrency;
        private object NewCurrency;

        private readonly INavigationService _navigationService;
        private readonly MonedasService _monedasService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<MonedaModel> Monedas;

        private string _filterText;
        private string _nombreMonedaIngresadoText;

        private string _editNombreMoneda;

        public ICommand CargarMonedaCommand { get; }
        public ICommand AgregarMonedaCommand { get; }
        public ICommand EliminarMonedaCommand { get; }
        public ICommand ActualizarMonedaCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Encapsulado
        public ObservableCollection<MonedaModel> MonedasInfoCollection
        {
            get { return Monedas; }
            set { Monedas = value; }
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

        public object OldCurrency
        {
            get => _oldCurrency;
            set
            {
                if (_oldCurrency != value)
                {
                    _oldCurrency = value;
                }
            }
        }

        public string NombreMonedaIngresadoText
        {
            get => _nombreMonedaIngresadoText;
            set
            {
                if (_nombreMonedaIngresadoText != value)
                {
                    _nombreMonedaIngresadoText = value;
                    OnPropertyChanged(nameof(NombreMonedaIngresadoText));
                }
            }
        }

        public string EditNombreMoneda
        {
            get => _editNombreMoneda;
            set
            {
                if (_editNombreMoneda != value)
                {
                    _editNombreMoneda = value;
                    OnPropertyChanged(nameof(EditNombreMoneda));
                }
            }
        }

        // Acción para establecer la lógica del filtro
        public Action ApplyFilterAction { get; set; }
        #endregion

        #region Constructores
        public MonedasViewModel(INavigationService navigationService)
        {
            _monedasService = new MonedasService();
            Monedas = new ObservableCollection<MonedaModel>();
            _navigationService = navigationService;

            CargarMonedas();

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
                if (e.RowData is MonedaModel moneda)
                {
                    IdMonedaCeldaSeleccionada = moneda.Id_Monedas;
                    NombreMonedaCeldaSeleccionada = moneda.Nombre;
                    // Aquí puedes manejar la lógica de negocio sin tocar la vista
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error CeldaTocada MonedasViewModel: " + ex.Message);
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
            CargarMonedas();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    EliminarMoneda(IdMonedaCeldaSeleccionada);
                    CargarMonedas();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
                Console.WriteLine("Error Eliminar MonedasViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void Agregar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Monedas");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                Console.WriteLine("Error Agregar MonedasViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void InsertarMoneda()
        {
            try
            {
                if (!string.IsNullOrEmpty(NombreMonedaIngresadoText))
                {
                    AgregarMoneda(new MonedaModel(0, NombreMonedaIngresadoText));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error InsertarMoneda MonedasViewModel: " + Ex.Message);
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
                        OldCurrency = new MonedaModel(IdMonedaCeldaSeleccionada,  NombreMonedaCeldaSeleccionada)
                        {
                            Nombre = NombreMonedaCeldaSeleccionada
                        };

                        await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Monedas", OldCurrency);
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
                Console.WriteLine("Error Editar MonedasViewModel: " + Ex.Message);
            }
        }

        private async Task CargarMonedas()
        {
            try
            {
                Console.WriteLine("MonedasInfoCollection: " + MonedasInfoCollection);
                var monedas = await _monedasService.GetMonedasAsync();
                Monedas.Clear();
                foreach (var moneda in monedas)
                {
                    Monedas.Add(moneda);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Editar MonedasViewModel: " + Ex.Message);
            }
        }

        private async Task AgregarMoneda(MonedaModel moneda)
        {
            try
            {
                if (await _monedasService.AddMonedasAsync(moneda))
                {
                    Monedas.Add(moneda);
                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente. ", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error, ya existe una entrada asignada con este Codigo.", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error AgregarMoneda MonedasViewModel: " + Ex.Message);
            }
        }

        private async Task EliminarMoneda(int id)
        {
            try
            {
                if (await _monedasService.DeleteMonedasAsync(id))
                {
                    var moneda = Monedas.FirstOrDefault(p => p.Id_Monedas == id);
                    if (moneda != null)
                    {
                        Monedas.Remove(moneda);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error EliminarMoneda MonedasViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public void Update()
        {
            try
            {
                ActualizarMoneda((MonedaModel)OldCurrency);
                Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");
                _navigationService.GoBackAsync();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Update MonedasViewModel: " + Ex.Message);
            }
        }

        private async Task ActualizarMoneda(MonedaModel AntiguoMoneda)
        {
            try
            {
                Console.WriteLine("EditNombreMoneda: " + EditNombreMoneda);

                var id = IdMonedaCeldaSeleccionada;
                var name = EditNombreMoneda;

                NewCurrency = new MonedaModel(id, name)
                {
                    Id_Monedas = id,
                    Nombre = name
                };

                if (await _monedasService.UpdateMonedasAsync((MonedaModel)NewCurrency))
                {
                    //Remove Old Product
                    Monedas.Remove(AntiguoMoneda);

                    //Add new product
                    Monedas.Add((MonedaModel)NewCurrency);

                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error ActualizarMoneda MonedasViewModel: " + Ex.Message);
            }
        }

        // Lógica de filtrado como delegado
        public Predicate<object> GetFilter()
        {
            return item =>
            {
                if (item is MonedaModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Id_Monedas.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Nombre.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }
        #endregion
    }
}
