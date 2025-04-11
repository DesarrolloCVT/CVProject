using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
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

namespace DesktopAplicationCV.ViewModel
{
    public partial class CuentasViewModel : BaseViewModel
    {
        #region Variables

        private static int IdCuentaSeleccionada;

        private static object _oldAccount;
        private object NewAccount;

        private int CodigoCeldaSeleccionada;
        private string NombreCuentaCeldaSeleccionada;
        private string TipoCuentaCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly CuentasService _cuentaService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<CuentasModel> Cuentas;

        private string _filterText;
        private int _codigoCuentaIngresadoText;
        private string _nombreCuentaIngresadoText;
        private string _tipoCuentaIngresadoText;

        private int _editCodigoCuenta;
        private string _editNombreCuenta;
        private string _editTipoCuenta;


        public ICommand CargarCuentasCommand { get; }
        public ICommand AgregarCuentaCommand { get; }
        public ICommand EliminarCuentaCommand { get; }
        public ICommand ActualizarCuentaCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Encapsulado

        public ObservableCollection<CuentasModel> CuentasInfoCollection
        {
            get { return Cuentas; }
            set { Cuentas = value; }
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

        // Acción para establecer la lógica del filtro
        public Action ApplyFilterAction { get; set; }

        public object OldAccount
        {
            get => _oldAccount;
            set
            {
                if (_oldAccount != value)
                {
                    _oldAccount = value;
                }
            }
        }

        public int CodigoCuentaIngresado
        {
            get => _codigoCuentaIngresadoText;
            set
            {
                if (_codigoCuentaIngresadoText != value)
                {
                    _codigoCuentaIngresadoText = value;
                    OnPropertyChanged(nameof(CodigoCuentaIngresado));
                }
            }
        }

        public int EditCodigoCuenta
        {
            get => _editCodigoCuenta;
            set
            {
                if (_editCodigoCuenta != value)
                {
                    _editCodigoCuenta = value;
                    OnPropertyChanged(nameof(EditCodigoCuenta));
                }
            }
        }

        public string NombreCuentaIngresado
        {
            get => _nombreCuentaIngresadoText;
            set
            {
                if (_nombreCuentaIngresadoText != value)
                {
                    _nombreCuentaIngresadoText = value;
                    OnPropertyChanged(nameof(NombreCuentaIngresado));
                }
            }
        }

        public string EditNombreCuenta
        {
            get => _editNombreCuenta;
            set
            {
                if (_editNombreCuenta != value)
                {
                    _editNombreCuenta = value;
                    OnPropertyChanged(nameof(EditNombreCuenta));
                }
            }
        }

        public string TipoCuentaIngresado
        {
            get => _tipoCuentaIngresadoText;
            set
            {
                if (_tipoCuentaIngresadoText != value)
                {
                    _tipoCuentaIngresadoText = value;
                    OnPropertyChanged(nameof(TipoCuentaIngresado));
                }
            }
        }

        public string EditTipoCuenta
        {
            get => _editTipoCuenta;
            set
            {
                if (_editTipoCuenta != value)
                {
                    _editTipoCuenta = value;
                    OnPropertyChanged(nameof(EditTipoCuenta));
                }
            }
        }

        #endregion

        #region Constructores
        public CuentasViewModel(INavigationService navigationService)
        {
            _cuentaService = new CuentasService();
            Cuentas = new ObservableCollection<CuentasModel>();

            _navigationService = navigationService;
            CargarCuentas();

            CeldaTocadaCommand = new Command<DataGridCellTappedEventArgs>(CeldaTocada);
        }
        #endregion

        #region Metodos 
        [RelayCommand]
        public void Cancelar()
        {
            _navigationService.GoBackAsync();
        }

        // Lógica de filtrado como delegado
        public Predicate<object> GetFilter()
        {
            return item =>
            {
                if (item is CuentasModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Codigo.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Nombre.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Tipo.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        // Método que se ejecuta cuando se toca una celda
        private void CeldaTocada(DataGridCellTappedEventArgs e)
        {
            try
            {
                if (e.RowData is CuentasModel cuentas)
                {
                    IdCuentaSeleccionada = cuentas.Id_Cuenta;
                    CodigoCeldaSeleccionada = cuentas.Codigo;
                    NombreCuentaCeldaSeleccionada = cuentas.Nombre;
                    TipoCuentaCeldaSeleccionada = cuentas.Tipo;
                    // Aquí puedes manejar la lógica de negocio sin tocar la vista
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error CeldaTocada CuentasViewModel: " + ex.Message);
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
            CargarCuentas();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    EliminarCuentas(IdCuentaSeleccionada);
                    CargarCuentas();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
                Console.WriteLine("Error Eliminar CuentasViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void Agregar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Cuentas");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                Console.WriteLine("Error Agregar CuentasViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void InsertarCuenta()
        {
            try
            {
                if (CodigoCuentaIngresado != 0 && !string.IsNullOrEmpty(NombreCuentaIngresado) && !string.IsNullOrEmpty(TipoCuentaIngresado))
                {
                    AgregarCuenta(new CuentasModel(IdCuentaSeleccionada, CodigoCuentaIngresado, NombreCuentaIngresado, TipoCuentaIngresado));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error InsertarCuenta CuentasViewModel: " + Ex.Message);
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
                        OldAccount = new CuentasModel(IdCuentaSeleccionada, CodigoCeldaSeleccionada, NombreCuentaCeldaSeleccionada, TipoCuentaCeldaSeleccionada)
                        {
                            Id_Cuenta = IdCuentaSeleccionada,
                            Codigo = CodigoCeldaSeleccionada,
                            Nombre = NombreCuentaCeldaSeleccionada,
                            Tipo = TipoCuentaCeldaSeleccionada
                        };

                        await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Cuentas", OldAccount);
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
                Console.WriteLine("Error InsertarCuenta CuentasViewModel: " + Ex.Message);
            }
        }

        private async Task CargarCuentas()
        {
            try
            {
                var cuentas = await _cuentaService.GetCuentasAsync();
                Cuentas.Clear();
                foreach (var cuenta in cuentas)
                {
                    Cuentas.Add(cuenta);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error InsertarCuenta CuentasViewModel: " + Ex.Message);
            }
        }

        private async Task AgregarCuenta(CuentasModel cuenta)
        {
            try
            {
                if (await _cuentaService.AddCuentaAsync(cuenta))
                {
                    Cuentas.Add(cuenta);
                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente. ", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error, ya existe una entrada asignada con este Codigo.", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error AgregarCuenta CuentasViewModel: " + Ex.Message);
            }
        }

        private async Task EliminarCuentas(int id)
        {
            try
            {
                if (await _cuentaService.DeleteCuentaAsync(id))
                {
                    var cuenta = Cuentas.FirstOrDefault(p => p.Id_Cuenta == id);
                    if (cuenta != null)
                    {
                        Cuentas.Remove(cuenta);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error EliminarCuentas CuentasViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public void Update()
        {
            try
            {
                ActualizarCuenta((CuentasModel)OldAccount);
                _navigationService.GoBackAsync();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Update CuentasViewModel: " + Ex.Message);
            }
        }

        private async Task ActualizarCuenta(CuentasModel AntiguaCuenta)
        {
            try
            {
                Console.WriteLine("EditCodigoCuenta: " + EditCodigoCuenta);
                Console.WriteLine("EditNombreCuenta: " + EditNombreCuenta);
                Console.WriteLine("EditTipoCuenta: " + EditTipoCuenta);

                var id = IdCuentaSeleccionada;
                var cod = EditCodigoCuenta;
                var name = EditNombreCuenta;
                var type = EditTipoCuenta;

                NewAccount = new CuentasModel(id, cod, name, type)
                {
                    Id_Cuenta = id,
                    Codigo = cod,
                    Nombre = name,
                    Tipo = type
                };

                if (await _cuentaService.UpdateCuentaAsync((CuentasModel)NewAccount))
                {
                    //Remove Old
                    Cuentas.Remove(AntiguaCuenta);

                    //Add new
                    Cuentas.Add((CuentasModel)NewAccount);
                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");

                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error al editar. ", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error ActualizarCuenta CuentasViewModel: " + Ex.Message);
            }
        }
        #endregion
    }
}
