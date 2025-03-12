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
    public partial class IngresoDetalleViewsModel : BaseViewModel
    {
        #region Variables

        private static object _oldIngresoDetalle;
        private object NewIngresoDetalle;

        private int FolioCeldaSeleccionada;
        private int MontoIngresoDetalleCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly IngresoDetalleService _ingresoDetalleService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<IngresoDetalleModel> IngresoDetalleModels;

        private string _filterText;

        private int _folioIngresoDetalleIngresado;
        private int _montoIngresoDetalleIngresado;

        private int _editFolioIngresoDetalle;
        private int _editMontoIngresoDetalle;
        

        public ICommand CargarIngresoDetalleCommand { get; }
        public ICommand AgregarIngresoDetalleCommand { get; }
        public ICommand EliminarIngresoDetalleCommand { get; }
        public ICommand ActualizarIngresoDetalleCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Encapsulado

        public ObservableCollection<IngresoDetalleModel> IngresoDetalleInfoCollection
        {
            get { return IngresoDetalleModels; }
            set { IngresoDetalleModels = value; }
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

        public object OldIngresoDetalle
        {
            get => _oldIngresoDetalle;
            set
            {
                if (_oldIngresoDetalle != value)
                {
                    _oldIngresoDetalle = value;
                }
            }
        }

        public int FolioIngresoDetalleIngresado
        {
            get => _folioIngresoDetalleIngresado;
            set
            {
                if (_folioIngresoDetalleIngresado != value)
                {
                    _folioIngresoDetalleIngresado = value;
                    OnPropertyChanged(nameof(FolioIngresoDetalleIngresado));
                }
            }
        }

        public int EditFolioIngresoDetalle
        {
            get => _editFolioIngresoDetalle;
            set
            {
                if (_editFolioIngresoDetalle != value)
                {
                    _editFolioIngresoDetalle = value;
                    OnPropertyChanged(nameof(EditFolioIngresoDetalle));
                }
            }
        }

        public int MontoIngresoDetalleIngresado
        {
            get => _montoIngresoDetalleIngresado;
            set
            {
                if (_montoIngresoDetalleIngresado != value)
                {
                    _montoIngresoDetalleIngresado = value;
                    OnPropertyChanged(nameof(MontoIngresoDetalleIngresado));
                }
            }
        }

        public int EditMontoIngresoDetalle
        {
            get => _editMontoIngresoDetalle;
            set
            {
                if (_editMontoIngresoDetalle != value)
                {
                    _editMontoIngresoDetalle = value;
                    OnPropertyChanged(nameof(EditMontoIngresoDetalle));
                }
            }
        }

        #endregion

        #region Constructores

        public IngresoDetalleViewsModel(INavigationService navigationService)
        {
            _ingresoDetalleService = new IngresoDetalleService();
            IngresoDetalleModels = new ObservableCollection<IngresoDetalleModel>();

            _navigationService = navigationService;
            CargarIngresoDetalles();

            CeldaTocadaCommand = new Command<DataGridCellTappedEventArgs>(CeldaTocada);
        }

        #endregion

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
                if (item is IngresoDetalleModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Folio_FacturaVenta.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
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
                if (e.RowData is IngresoDetalleModel ingreso)
                {
                    FolioCeldaSeleccionada = ingreso.Folio_FacturaVenta;
                    MontoIngresoDetalleCeldaSeleccionada = ingreso.Monto;
                    // Aquí puedes manejar la lógica de negocio sin tocar la vista
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error CeldaTocada IngresoDetalleViewsModel: " + ex.Message);
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
            CargarIngresoDetalles();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    EliminarIngresoDetalle(FolioCeldaSeleccionada);
                    CargarIngresoDetalles();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
                Console.WriteLine("Error Eliminar IngresoDetalleViewsModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void Agregar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Ingresos_Detalle");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                Console.WriteLine("Error Agregar IngresoDetalleViewsModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void InsertarIngresoDetalle()
        {
            try
            {
                if (FolioIngresoDetalleIngresado != 0 && MontoIngresoDetalleIngresado != 0)
                {
                    AgregarIngresoDetalle(new IngresoDetalleModel(FolioIngresoDetalleIngresado, MontoIngresoDetalleIngresado));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error InsertarIngresoDetalle IngresoDetalleViewsModel: " + Ex.Message);
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
                        OldIngresoDetalle = new IngresoDetalleModel(FolioCeldaSeleccionada, MontoIngresoDetalleCeldaSeleccionada)
                        {
                            Folio_FacturaVenta = FolioCeldaSeleccionada,
                            Monto = MontoIngresoDetalleCeldaSeleccionada
                        };

                        await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Ingresos_Detalle", OldIngresoDetalle);
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
                Console.WriteLine("Error Editar IngresoDetalleViewsModel: " + Ex.Message);
            }
        }

        private async Task CargarIngresoDetalles()
        {
            try
            {
                var ingresoDetalle = await _ingresoDetalleService.GetIngresoDetalleAsync();
                IngresoDetalleModels.Clear();
                foreach (var ingreso in ingresoDetalle)
                {
                    IngresoDetalleModels.Add(ingreso);
                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine("Error CargarIngresoDetalles IngresoDetalleViewsModel: " + Ex.Message);
            }
        }

        private async Task AgregarIngresoDetalle(IngresoDetalleModel ingresoDetalle)
        {
            try
            {
                if (await _ingresoDetalleService.AddIngresoDetalleAsync(ingresoDetalle))
                {
                    IngresoDetalleModels.Add(ingresoDetalle);
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error, ya existe una entrada asignada con este Folio.", "Ok");
                }
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error AgregarIngresoDetalle IngresoDetalleViewsModel: " + Ex.Message);
            }
        }

        private async Task EliminarIngresoDetalle(int folio)
        {
            try
            {
                if (await _ingresoDetalleService.DeleteIngresoDetalleAsync(folio))
                {
                    var ingreso = IngresoDetalleModels.FirstOrDefault(p => p.Folio_FacturaVenta == folio);
                    if (ingreso != null)
                    {
                        IngresoDetalleModels.Remove(ingreso);
                    }
                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine("Error EliminarIngresoDetalle IngresoDetalleViewsModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public void Update()
        {
            try
            {
                ActualizarIngresoDetalle((IngresoDetalleModel)OldIngresoDetalle);
                Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");
                _navigationService.GoBackAsync();
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error Update IngresoDetalleViewsModel: " + Ex.Message);
            }
        }

        private async Task ActualizarIngresoDetalle(IngresoDetalleModel AntiguoIngresoDetalle)
        {
            try
            {
                Console.WriteLine("EditFolioIngresoDetalle: " + EditFolioIngresoDetalle);
                Console.WriteLine("EditMontoIngresoDetalle: " + EditMontoIngresoDetalle);

                var folio = EditFolioIngresoDetalle;
                var monto = EditMontoIngresoDetalle;

                NewIngresoDetalle = new IngresoDetalleModel(folio, monto)
                {
                    Folio_FacturaVenta = folio,
                    Monto = monto
                };

                if (await _ingresoDetalleService.UpdateIngresoDetalleAsync((IngresoDetalleModel)NewIngresoDetalle))
                {
                    //Remove Old
                    IngresoDetalleModels.Remove(AntiguoIngresoDetalle);

                    //Add new
                    IngresoDetalleModels.Add((IngresoDetalleModel)NewIngresoDetalle);

                }
            }
            catch (Exception Ex) 
            {
                Console.WriteLine("Error ActualizarIngresoDetalle IngresoDetalleViewsModel: " + Ex.Message);
            }
        }
    }
}
