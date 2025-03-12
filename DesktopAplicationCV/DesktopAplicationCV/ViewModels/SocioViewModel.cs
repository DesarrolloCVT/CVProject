using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using Syncfusion.Maui.DataGrid;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DesktopAplicationCV.ViewModel
{
    public partial class SocioViewModel : BaseViewModel
    {
        #region Variables

        private static object _oldSocio;
        private object NewSocio;

        private int CodigoCeldaSeleccionada;
        private string NombreSocioCeldaSeleccionada;
        private string TipoSocioCeldaSeleccionada;
        private int SaldoSocioCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly SocioNegocioService _socioNegocioService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<SocioNegocioModel> Socios;

        private string _filterText;
        private int _codigoSocioIngresadoText;
        private string _nombreSocioIngresadoText;
        private string _tipoSocioIngresadoText;
        private int _saldoSocioIngresadoText;

        private int _editCodigoSocio;
        private string _editNombreSocio;
        private string _editTipoSocio;
        private int _editSaldoSocio;

        public ICommand CargarSociosCommand { get; }
        public ICommand AgregarSocioCommand { get; }
        public ICommand EliminarSocioCommand { get; }
        public ICommand ActualizarSocioCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Encapsulado

        public ObservableCollection<SocioNegocioModel> SocioInfoCollection
        {
            get { return Socios; }
            set { Socios = value; }
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

        public object OldSocio
        {
            get => _oldSocio;
            set
            {
                if (_oldSocio != value)
                {
                    _oldSocio = value;
                }
            }
        }

        public int CodigoSocioIngresado
        {
            get => _codigoSocioIngresadoText;
            set
            {
                if (_codigoSocioIngresadoText != value)
                {
                    _codigoSocioIngresadoText = value;
                    OnPropertyChanged(nameof(CodigoSocioIngresado));
                }
            }
        }

        public int EditCodigoSocio
        {
            get => _editCodigoSocio;
            set
            {
                if (_editCodigoSocio != value)
                {
                    _editCodigoSocio = value;
                    OnPropertyChanged(nameof(EditCodigoSocio));
                }
            }
        }

        public string NombreSocioIngresado
        {
            get => _nombreSocioIngresadoText;
            set
            {
                if (_nombreSocioIngresadoText != value)
                {
                    _nombreSocioIngresadoText = value;
                    OnPropertyChanged(nameof(NombreSocioIngresado));
                }
            }
        }

        public string EditNombreSocio
        {
            get => _editNombreSocio;
            set
            {
                if (_editNombreSocio != value)
                {
                    _editNombreSocio = value;
                    OnPropertyChanged(nameof(EditNombreSocio));
                }
            }
        }

        public string TipoSocioIngresado
        {
            get => _tipoSocioIngresadoText;
            set
            {
                if (_tipoSocioIngresadoText != value)
                {
                    _tipoSocioIngresadoText = value;
                    OnPropertyChanged(nameof(TipoSocioIngresado));
                }
            }
        }

        public string EditTipoSocio
        {
            get => _editTipoSocio;
            set
            {
                if (_editTipoSocio != value)
                {
                    _editTipoSocio = value;
                    OnPropertyChanged(nameof(EditTipoSocio));
                }
            }
        }

        public int SaldoSocioIngresado
        {
            get => _saldoSocioIngresadoText;
            set
            {
                if (_saldoSocioIngresadoText != value)
                {
                    _saldoSocioIngresadoText = value;
                    OnPropertyChanged(nameof(SaldoSocioIngresado));
                }
            }
        }

        public int EditSaldoSocio
        {
            get => _editSaldoSocio;
            set
            {
                if (_editSaldoSocio != value)
                {
                    _editSaldoSocio = value;
                    OnPropertyChanged(nameof(EditSaldoSocio));
                }
            }
        }

        // Acción para establecer la lógica del filtro
        public Action ApplyFilterAction { get; set; }

        #endregion

        #region Constructores

        public SocioViewModel(INavigationService navigationService)
        {
            _socioNegocioService = new SocioNegocioService();
            Socios = new ObservableCollection<SocioNegocioModel>();

            _navigationService = navigationService;
            CargarSocios();

            CeldaTocadaCommand = new Command<DataGridCellTappedEventArgs>(CeldaTocada);
        }

        #endregion

        // Lógica de filtrado como delegado
        public Predicate<object> GetFilter()
        {
            return item =>
            {
                if (item is SocioNegocioModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Nombre.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Codigo.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Tipo.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Saldo.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        // Método que se ejecuta cuando se toca una celda
        private void CeldaTocada(DataGridCellTappedEventArgs e)
        {
            try
            {
                if (e.RowData is SocioNegocioModel socios)
                {
                    CodigoCeldaSeleccionada = socios.Codigo;
                    NombreSocioCeldaSeleccionada = socios.Nombre;
                    TipoSocioCeldaSeleccionada = socios.Tipo;
                    SaldoSocioCeldaSeleccionada = socios.Saldo;
                    // Aquí puedes manejar la lógica de negocio sin tocar la vista
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error CeldaTocada SocioViewModel: " + ex.Message);
            }
            
        }

        #region Binding Methods 

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [RelayCommand]
        public void Cancelar()
        {
            _navigationService.GoBackAsync();
        }

        [RelayCommand]
        public void GridCargado()
        {
            CargarSocios();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    EliminarSocio(CodigoCeldaSeleccionada);
                    CargarSocios();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
                Console.WriteLine("Error Eliminar SocioViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void Agregar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Socio_Negocio");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                Console.WriteLine("Error Agregar SocioViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void InsertarSocio()
        {
            try
            {
                if (CodigoSocioIngresado != 0 && !string.IsNullOrEmpty(NombreSocioIngresado) && !string.IsNullOrEmpty(TipoSocioIngresado))
                {
                    AgregarSocio(new SocioNegocioModel(CodigoSocioIngresado, NombreSocioIngresado, TipoSocioIngresado, SaldoSocioIngresado));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine("Error InsertarSocio SocioViewModel: " + Ex.Message);
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
                        OldSocio = new SocioNegocioModel(CodigoCeldaSeleccionada, NombreSocioCeldaSeleccionada, TipoSocioCeldaSeleccionada, SaldoSocioCeldaSeleccionada)
                        {
                            Codigo = CodigoCeldaSeleccionada,
                            Nombre = NombreSocioCeldaSeleccionada,
                            Tipo = TipoSocioCeldaSeleccionada,
                            Saldo = SaldoSocioCeldaSeleccionada
                        };

                        await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Socio_Negocio", OldSocio);
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
            catch(Exception Ex)
            {
                Console.WriteLine("Error Editar SocioViewModel: " + Ex.Message);
            }
        }

        private async Task CargarSocios()
        {
            try
            {
                var socios = await _socioNegocioService.GetSociosAsync();
                Socios.Clear();
                foreach (var socio in socios)
                {
                    Socios.Add(socio);
                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine("Error CargarSocios SocioViewModel: " + Ex.Message);
            }
        }

        private async Task AgregarSocio(SocioNegocioModel socio)
        {
            try
            {
                if (await _socioNegocioService.AddSocioAsync(socio))
                {
                    Socios.Add(socio);
                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente. ", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error, ya existe una entrada asignada con este Codigo.", "Ok");
                }
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error AgregarSocio SocioViewModel: " + Ex.Message);
            }
        }

        private async Task EliminarSocio(int codigo)
        {
            try
            {
                if (await _socioNegocioService.DeleteSocioAsync(codigo))
                {
                    var socio = Socios.FirstOrDefault(p => p.Codigo == codigo);
                    if (socio != null)
                    {
                        Socios.Remove(socio);
                    }
                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine("Error EliminarSocio SocioViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public void Update()
        {
            try
            {
                ActualizarSocio((SocioNegocioModel)OldSocio);
                Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");
                _navigationService.GoBackAsync();
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error Update SocioViewModel: " + Ex.Message);
            }
        }
        #endregion

        private async Task ActualizarSocio(SocioNegocioModel AntiguoSocio)
        {
            try
            {
                Console.WriteLine("EditCodigoSocio: " + EditCodigoSocio);
                Console.WriteLine("EditNombreSocio: " + EditNombreSocio);
                Console.WriteLine("EditTipoSocio: " + EditTipoSocio);

                var cod = EditCodigoSocio;
                var nombre = EditNombreSocio;
                var tipo = EditTipoSocio;
                var saldo = SaldoSocioCeldaSeleccionada;

                NewSocio = new SocioNegocioModel(cod, nombre, tipo, saldo)
                {
                    Codigo = cod,
                    Nombre = nombre,
                    Tipo = tipo,
                    Saldo = saldo
                };

                if (await _socioNegocioService.UpdateSocioAsync((SocioNegocioModel)OldSocio, (SocioNegocioModel)NewSocio))
                {
                    //Remove Old Socio
                    Socios.Remove((SocioNegocioModel)OldSocio);

                    //Add new Socio
                    Socios.Add((SocioNegocioModel)NewSocio);

                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine("Error ActualizarSocio SocioViewModel: " + Ex.Message);
            }
        }
    }
}
