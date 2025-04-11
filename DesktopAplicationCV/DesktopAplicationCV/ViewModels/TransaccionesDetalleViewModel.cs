using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopAplicationCV.Models;
using DesktopAplicationCV.Services;
using DesktopAplicationCV.ViewModel;
using DesktopAplicationCV.Views;
using Syncfusion.Maui.DataGrid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopAplicationCV.ViewModels
{
    public partial class TransaccionesDetalleViewModel : BaseViewModel
    {
        #region Variables
        private static object _oldTransaccionDetalle;
        private object NewTransaccionDetalle;

        public int Id_Transaccion;
        private static int IdTransaccionDetalleCeldaSeleccionada;
        private int? IdTransaccionCeldaSeleccionada;
        private int FolioFacturaTransaccionDetalleCeldaSeleccionada;
        private string TipoFacturaTransaccionDetalleCeldaSeleccionada;
        private int MontoTransaccionDetalleCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly AuxService _auxService;
        private readonly TransaccionDetalleService _transaccionDetalleService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<TransaccionesDetalleModel> TransaccionesDetalle;

        private string _filterText;
        private string _tituloPagina;

        private int? _idTransaccionIngresadoText;
        private int _folioFacturaTransaccionDetalleIngresadoText;
        private string _tipoFacturaTransaccionDetalleIngresadoText;
        private string _montoTransaccionDetalleFormateado;
        //private int _montoTransaccionDetalleIngresadoText;


        private int _editIdTransaccion;
        private int _editFolioFacturaTransaccionDetalle;
        private string _editTipoFacturaTransaccionDetalle;
        private int _editMontoTransaccionDetalle;

        public ICommand CargarTransaccionDetalleCommand { get; }
        public ICommand AgregarTransaccionDetalleCommand { get; }
        public ICommand EliminarTransaccionDetalleCommand { get; }
        public ICommand ActualizarTransaccionDetalleCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Encapsulado

        public ObservableCollection<TransaccionesDetalleModel> TransaccionesDetalleInfoCollection
        {
            get { return TransaccionesDetalle; }
            set { TransaccionesDetalle = value; }
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

        public string TituloPagina
        {
            get => _tituloPagina;
            set
            {
                _tituloPagina = value;
                OnPropertyChanged(nameof(TituloPagina));
            }
        }

        public object OldTransaccionDetalle
        {
            get => _oldTransaccionDetalle;
            set
            {
                if (_oldTransaccionDetalle != value)
                {
                    _oldTransaccionDetalle = value;
                }
            }
        }

        public int? IdTransaccionIngresadoText
        {
            get => _idTransaccionIngresadoText;
            set
            {
                if (_idTransaccionIngresadoText != value)
                {
                    _idTransaccionIngresadoText = value;
                    OnPropertyChanged(nameof(IdTransaccionIngresadoText));
                }
            }
        }

        public int EditIdTransaccion
        {
            get => _editIdTransaccion;
            set
            {
                if (_editIdTransaccion != value)
                {
                    _editIdTransaccion = value;
                    OnPropertyChanged(nameof(EditIdTransaccion));
                }
            }
        }

        public int FolioFacturaTransaccionDetalleIngresadoText
        {
            get => _folioFacturaTransaccionDetalleIngresadoText;
            set
            {
                if (_folioFacturaTransaccionDetalleIngresadoText != value)
                {
                    _folioFacturaTransaccionDetalleIngresadoText = value;
                    OnPropertyChanged(nameof(FolioFacturaTransaccionDetalleIngresadoText));
                }
            }
        }

        public int EditFolioFacturaTransaccionDetalle
        {
            get => _editFolioFacturaTransaccionDetalle;
            set
            {
                if (_editFolioFacturaTransaccionDetalle != value)
                {
                    _editFolioFacturaTransaccionDetalle = value;
                    OnPropertyChanged(nameof(EditFolioFacturaTransaccionDetalle));
                }
            }
        }

        public string TipoFacturaTransaccionDetalleIngresadoText
        {
            get => _tipoFacturaTransaccionDetalleIngresadoText;
            set
            {
                if (_tipoFacturaTransaccionDetalleIngresadoText != value)
                {
                    _tipoFacturaTransaccionDetalleIngresadoText = value;
                    OnPropertyChanged(nameof(TipoFacturaTransaccionDetalleIngresadoText));
                }
            }
        }

        public string EditTipoFacturaTransaccionDetalle
        {
            get => _editTipoFacturaTransaccionDetalle;
            set
            {
                if (_editTipoFacturaTransaccionDetalle != value)
                {
                    _editTipoFacturaTransaccionDetalle = value;
                    OnPropertyChanged(nameof(EditTipoFacturaTransaccionDetalle));
                }
            }
        }

        public string MontoTransaccionDetalleFormateado
        {
            get => _montoTransaccionDetalleFormateado;
            set
            {
                if (_montoTransaccionDetalleFormateado != value)
                {
                    _montoTransaccionDetalleFormateado = value;
                    OnPropertyChanged(nameof(MontoTransaccionDetalleFormateado));
                }
            }
        }

        /*public int MontoTransaccionDetalleIngresadoText
        {
            get => _montoTransaccionDetalleIngresadoText;
            set
            {
                if (_montoTransaccionDetalleIngresadoText != value)
                {
                    _montoTransaccionDetalleIngresadoText = value;
                    OnPropertyChanged(nameof(MontoTransaccionDetalleIngresadoText));
                }
            }
        }*/

        public int EditMontoTransaccionDetalle
        {
            get => _editMontoTransaccionDetalle;
            set
            {
                if (_editMontoTransaccionDetalle != value)
                {
                    _editMontoTransaccionDetalle = value;
                    OnPropertyChanged(nameof(EditMontoTransaccionDetalle));
                }
            }
        }

        public int MontoTransaccionDetalleIngresadoText =>
    int.TryParse(MontoTransaccionDetalleFormateado, NumberStyles.Currency, CultureInfo.CurrentCulture, out var resultado)
        ? (int)resultado
        : 0;
        #endregion

        #region Constructores

        public TransaccionesDetalleViewModel(INavigationService navigationService, AuxService auxService)
        {
            _auxService = auxService;
            _transaccionDetalleService = new TransaccionDetalleService();
            TransaccionesDetalle = new ObservableCollection<TransaccionesDetalleModel>();

            _navigationService = navigationService;
            CargarGrillaIngresos();

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
                if (item is TransaccionesDetalleModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Id_Transaccion_Detalle.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Id_Transaccion.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Folio_Factura.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Tipo_Factura.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Monto.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        // Método que se ejecuta cuando se toca una celda
        private void CeldaTocada(DataGridCellTappedEventArgs e)
        {
            try
            {
                if (e.RowData is TransaccionesDetalleModel transaccionesDetalle)
                {
                    IdTransaccionDetalleCeldaSeleccionada = transaccionesDetalle.Id_Transaccion_Detalle;
                    IdTransaccionCeldaSeleccionada = transaccionesDetalle.Id_Transaccion;
                    FolioFacturaTransaccionDetalleCeldaSeleccionada = transaccionesDetalle.Folio_Factura;
                    TipoFacturaTransaccionDetalleCeldaSeleccionada = transaccionesDetalle.Tipo_Factura;
                    MontoTransaccionDetalleCeldaSeleccionada = transaccionesDetalle.Monto;
                    // Aquí puedes manejar la lógica de negocio sin tocar la vista
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error CeldaTocada TransaccionesViewModel: " + ex.Message);
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
            CargarGrillaIngresos();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    EliminarTransaccionesDetalle(IdTransaccionDetalleCeldaSeleccionada);
                    CargarGrillaIngresos();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
                Console.WriteLine("Error Eliminar TransaccionesViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void Agregar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Transacciones_Detalle");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                Console.WriteLine("Error Agregar TransaccionesViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void InsertarTransaccionesDetalle()
        {
            try
            {
                if (/*IdTransaccionIngresadoText != 0 && */ FolioFacturaTransaccionDetalleIngresadoText != 0 && !string.IsNullOrEmpty(TipoFacturaTransaccionDetalleIngresadoText) && MontoTransaccionDetalleIngresadoText != 0)
                {
                    AgregarTransaccionesDetalle(new TransaccionesDetalleModel(IdTransaccionDetalleCeldaSeleccionada, IdTransaccionIngresadoText, FolioFacturaTransaccionDetalleIngresadoText, TipoFacturaTransaccionDetalleIngresadoText, MontoTransaccionDetalleIngresadoText));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error InsertarTransaccionesDetalle IngresosViewModel: " + Ex.Message);
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
                        OldTransaccionDetalle = 
                            new TransaccionesDetalleModel(IdTransaccionDetalleCeldaSeleccionada, 
                            IdTransaccionCeldaSeleccionada, FolioFacturaTransaccionDetalleCeldaSeleccionada,
                            TipoFacturaTransaccionDetalleCeldaSeleccionada, 
                            MontoTransaccionDetalleCeldaSeleccionada)
                        {
                            Id_Transaccion = IdTransaccionDetalleCeldaSeleccionada,
                            Folio_Factura = FolioFacturaTransaccionDetalleCeldaSeleccionada,
                            Tipo_Factura = TipoFacturaTransaccionDetalleCeldaSeleccionada,
                            Monto = MontoTransaccionDetalleCeldaSeleccionada
                        };

                        await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Transacciones_Detalle", OldTransaccionDetalle);
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
                Console.WriteLine("Error Editar TransaccionesDetalleViewModel: " + Ex.Message);
            }
        }

        private async Task CargarGrillaIngresos()
        {
            try
            {
                var transaccionesDetalles = await _transaccionDetalleService.GetTransaccionDetalleFilterByIdAsync(Id_Transaccion);
                TransaccionesDetalle.Clear();
                foreach (var transaccionDetalle in transaccionesDetalles)
                {
                    TransaccionesDetalle.Add(transaccionDetalle);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error CargarGrillaTransaccionesDetalle TransaccionDetalleViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void Volver()
        {
            try
            {
                await _navigationService.GoBackAsync();
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error. ", "Ok");
                Console.WriteLine("Error Volver IngresoDetalleViewsModel: " + Ex.Message);
            }
        }

        private async Task AgregarTransaccionesDetalle(TransaccionesDetalleModel transaccionesDetalle)
        {
            try
            {
                if (await _transaccionDetalleService.AddTransaccionDetalleAsync(transaccionesDetalle))
                {
                    TransaccionesDetalle.Add(transaccionesDetalle);
                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error, ya existe una entrada asignada con este Folio.", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error AgregarTransaccionesDetalle TransaccionesDetalleViewModel: " + Ex.Message);
            }
        }

        private async Task EliminarTransaccionesDetalle(int id)
        {
            try
            {
                if (await _transaccionDetalleService.DeleteTransaccionDetalleAsync(id))
                {
                    var transaccionDetalle = TransaccionesDetalle.FirstOrDefault(p => p.Id_Transaccion_Detalle == id);
                    if (transaccionDetalle != null)
                    {
                        TransaccionesDetalle.Remove(transaccionDetalle);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error EliminarTransaccionesDetalle ViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public void Update()
        {
            try
            {
                ActualizarTransaccionesDetalle((TransaccionesDetalleModel)OldTransaccionDetalle);
                Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");
                _navigationService.GoBackAsync();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Update TransaccionesDetalleViewModel: " + Ex.Message);
            }

        }

        private async Task ActualizarTransaccionesDetalle(TransaccionesDetalleModel AntiguasTransaccionesDetalle)
        {
            try
            {
                Console.WriteLine("EditIdTransaccion: " + EditIdTransaccion);
                Console.WriteLine("EditFolioFacturaTransaccionDetalle: " + EditFolioFacturaTransaccionDetalle);
                Console.WriteLine("EditTipoFacturaTransaccionDetalle: " + EditTipoFacturaTransaccionDetalle);
                Console.WriteLine("MontoTransaccionDetalleCeldaSeleccionada: " + MontoTransaccionDetalleCeldaSeleccionada);

                var id = IdTransaccionDetalleCeldaSeleccionada;
                var idTransaccion = EditIdTransaccion;
                var folioFactura = EditFolioFacturaTransaccionDetalle;
                var tipoFactura = EditTipoFacturaTransaccionDetalle;
                var monto = EditMontoTransaccionDetalle;

                /*var date = fecha.ToString("yyyy-MM-dd",CultureInfo.InvariantCulture);
                var NewDate = Convert.ToDateTime(date);*/


                NewTransaccionDetalle = new TransaccionesDetalleModel(id, idTransaccion, folioFactura, tipoFactura, monto)
                {
                    Id_Transaccion_Detalle = id,
                    Id_Transaccion = idTransaccion,
                    Folio_Factura = folioFactura,
                    Tipo_Factura = tipoFactura,
                    Monto = monto
                };

                if (await _transaccionDetalleService.UpdateTransaccionAsync((TransaccionesDetalleModel)NewTransaccionDetalle))
                {
                    //Remove Old Product
                    TransaccionesDetalle.Remove(AntiguasTransaccionesDetalle);

                    //Add new product
                    TransaccionesDetalle.Add((TransaccionesDetalleModel)NewTransaccionDetalle);

                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error ActualizarTransaccionesDetalle TransaccionesDetalleViewModel: " + Ex.Message);
            }
        }
        #endregion
    }
}
