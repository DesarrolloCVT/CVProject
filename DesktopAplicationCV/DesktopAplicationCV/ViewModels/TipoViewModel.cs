using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.Views;
using Syncfusion.Maui.DataGrid;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DesktopAplicationCV.ViewModel
{
    public partial class TipoViewModel : BaseViewModel
    {
        #region Variables

        private const int MAX_INT = 2147483647; // Máximo permitido en SQL Server para INT

        private static object _oldType;
        private object NewType;

        private int CodigoCeldaSeleccionada;
        private string NombreTipoCeldaSeleccionada;
        private string TipoCeldaSeleccionada;
        private int CuentaTipoCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly TipoService _tipoService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<TipoModel> Tipos;

        private string _filterText;

        private int _codigoTipoIngresadoText;
        private string _nombreTipoIngresadoText;
        private string _tipoIngresadoText;
        private int _cuentaTipoIngresadoText;

        private int _editCodigoTipo;
        private string _editNombreTipo;
        private string _editTipo;
        private int _editCuentaTipo;

        public ICommand CargarTiposCommand { get; }
        public ICommand AgregarTipoCommand { get; }
        public ICommand EliminarTipoCommand { get; }
        public ICommand ActualizarTipoCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Encapsulado

        public ObservableCollection<TipoModel> TipoInfoCollection
        {
            get { return Tipos; }
            set { Tipos = value; }
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

        public object OldType
        {
            get => _oldType;
            set
            {
                if (_oldType != value)
                {
                    _oldType = value;
                }
            }
        }

        public int CodigoTipoIngresado
        {
            get => _codigoTipoIngresadoText;
            set
            {
                if (_codigoTipoIngresadoText != value)
                {
                    _codigoTipoIngresadoText = value;
                    OnPropertyChanged(nameof(CodigoTipoIngresado));
                }
            }
        }

        public string NombreTipoIngresado
        {
            get => _nombreTipoIngresadoText;
            set
            {
                if (_nombreTipoIngresadoText != value)
                {
                    _nombreTipoIngresadoText = value;
                    OnPropertyChanged(nameof(NombreTipoIngresado));
                }
            }
        }

        public string TipoIngresado
        {
            get => _tipoIngresadoText;
            set
            {
                if (_tipoIngresadoText != value)
                {
                    _tipoIngresadoText = value;
                    OnPropertyChanged(nameof(TipoIngresado));
                }
            }
        }

        public int CuentaTipoIngresado
        {
            get => _cuentaTipoIngresadoText;
            set
            {
                if (_cuentaTipoIngresadoText != value)
                {
                    _cuentaTipoIngresadoText = value;
                    OnPropertyChanged(nameof(CuentaTipoIngresado));
                }
            }
        }

        public int EditCodigoTipo
        {
            get => _editCodigoTipo;
            set
            {
                if (_editCodigoTipo != value)
                {
                    _editCodigoTipo = value;
                    OnPropertyChanged(nameof(EditCodigoTipo));
                }
            }
        }

        public string EditNombreTipo
        {
            get => _editNombreTipo;
            set
            {
                if (_editNombreTipo != value)
                {
                    _editNombreTipo = value;
                    OnPropertyChanged(nameof(EditNombreTipo));
                }
            }
        }

        public string EditTipo
        {
            get => _editTipo;
            set
            {
                if (_editTipo != value)
                {
                    _editTipo = value;
                    OnPropertyChanged(nameof(EditNombreTipo));
                }
            }
        }

        public int EditCuentaTipo
        {
            get => _editCuentaTipo;
            set
            {
                if (_editCuentaTipo != value)
                {
                    _editCuentaTipo = value;
                    OnPropertyChanged(nameof(EditNombreTipo));
                }
            }
        }

        #endregion

        #region Constructores

        public TipoViewModel(INavigationService navigationService)
        {
            _tipoService = new TipoService();
            Tipos = new ObservableCollection<TipoModel>();

            _navigationService = navigationService;
            CargarTipos();

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
                if (item is TipoModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Codigo.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Nombre.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Tipo_Dato.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Cuenta.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        // Método que se ejecuta cuando se toca una celda
        private void CeldaTocada(DataGridCellTappedEventArgs e)
        {
            try
            {
                if (e.RowData is TipoModel tipos)
                {
                    CodigoCeldaSeleccionada = tipos.Codigo;
                    NombreTipoCeldaSeleccionada = tipos.Nombre;
                    TipoCeldaSeleccionada = tipos.Tipo_Dato;
                    CuentaTipoCeldaSeleccionada = tipos.Cuenta;
                    // Aquí puedes manejar la lógica de negocio sin tocar la vista
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error CeldaTocada TipoViewModel: " + ex.Message);
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
            CargarTipos();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    EliminarTipo(CodigoCeldaSeleccionada);
                    CargarTipos();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
                Console.WriteLine("Error Eliminar TipoViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void Agregar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Tipo");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                Console.WriteLine("Error Agregar TipoViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void InsertarTipo()
        {
            try
            {
                if (CodigoTipoIngresado != 0 && !string.IsNullOrEmpty(NombreTipoIngresado)
                && !string.IsNullOrEmpty(TipoIngresado) && CuentaTipoIngresado != 0)
                {
                    AgregarTipo(new TipoModel(CodigoTipoIngresado, NombreTipoIngresado, TipoIngresado,
                        CuentaTipoIngresado));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error InsertarTipo TipoViewModel: " + Ex.Message);
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
                        OldType = new TipoModel(CodigoCeldaSeleccionada, NombreTipoCeldaSeleccionada,
                            TipoCeldaSeleccionada, CuentaTipoCeldaSeleccionada)
                        {
                            Codigo = CodigoCeldaSeleccionada,
                            Nombre = NombreTipoCeldaSeleccionada,
                            Tipo_Dato = TipoCeldaSeleccionada,
                            Cuenta = CuentaTipoCeldaSeleccionada
                        };

                        await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Tipo", OldType);
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
                Console.WriteLine("Error Editar TipoViewModel: " + Ex.Message);
            }
        }

        private async Task CargarTipos()
        {
            try
            {
                var tipos = await _tipoService.GetTipoAsync();
                Tipos.Clear();
                foreach (var tipo in tipos)
                {
                    Tipos.Add(tipo);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error CargarTipos TipoViewModel: " + Ex.Message);
            }
        }

        private async Task AgregarTipo(TipoModel tipo)
        {
            try
            {
                if (await _tipoService.AddTipoAsync(tipo))
                {
                    Tipos.Add(tipo);
                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente. ", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error, ya existe una entrada asignada con este Codigo.", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error AgregarTipo TipoViewModel: " + Ex.Message);
            }
        }

        private async Task EliminarTipo(int codigo)
        {
            try
            {
                if (await _tipoService.DeleteTipoAsync(codigo))
                {
                    var tipo = Tipos.FirstOrDefault(p => p.Codigo == codigo);
                    if (tipo != null)
                    {
                        Tipos.Remove(tipo);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error EliminarTipo TipoViewModel: " + Ex.Message);
            }

        }

        [RelayCommand]
        public void Update()
        {
            try
            {
                ActualizarTipo((TipoModel)OldType);
                _navigationService.GoBackAsync();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Update TipoViewModel: " + Ex.Message);
            }
        }

        private async Task ActualizarTipo(TipoModel AntiguoProducto)
        {
            try
            {
                Console.WriteLine("EditCodigoTipo: " + EditCodigoTipo);
                Console.WriteLine("EditNombreTipo: " + EditNombreTipo);
                Console.WriteLine("EditTipo: " + EditTipo);
                Console.WriteLine("EditCuentaTipo: " + EditCuentaTipo);

                var cod = EditCodigoTipo;
                var name = EditNombreTipo;
                var type = EditTipo;
                var account = EditCuentaTipo;


                NewType = new TipoModel(cod, name, type, account)
                {
                    Codigo = cod,
                    Nombre = name,
                    Tipo_Dato = type,
                    Cuenta = account
                };

                if (await _tipoService.UpdateTipoAsync((TipoModel)NewType))
                {
                    //Remove Old Product
                    Tipos.Remove(AntiguoProducto);
                    //Add new product
                    Tipos.Add((TipoModel)NewType);
                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error ActualizarTipo TipoViewModel: " + Ex.Message);
            }
        }
        #endregion
    }
}