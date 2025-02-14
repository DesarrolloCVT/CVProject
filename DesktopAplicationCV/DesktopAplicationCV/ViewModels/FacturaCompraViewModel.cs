﻿using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class FacturaCompraViewModel : BaseViewModel
    {
        #region Variables

        private static object _oldFacturaCompra;
        private object NewFacturaCompra;

        private int FolioCeldaSeleccionada;
        private string ProveedorFactCompraCeldaSeleccionada;
        private DateTime FechaFactCompraCeldaSeleccionada;
        private string MonedaFactCompraCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly FacturaCompraService _facturaCompraService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<FacturaCompraModel> facturaCompra;

        private string _filterText;

        private int _folioFactCompraIngresadoText;
        private string _proveedorFactCompraIngresadoText;
        private DateTime _fechaFactCompraIngresadoText;
        private string _monedaFactCompraIngresadoText;
        
        private int _editFolioFactCompra;
        private string _editProveedorFactCompra;
        private DateTime _editFechaFactCompra;
        private string _editMonedaFactCompra;

        public ICommand CargarFactComprasCommand { get; }
        public ICommand AgregarFactCompraCommand { get; }
        public ICommand EliminarFactCompraCommand { get; }
        public ICommand ActualizarFactCompraCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Inicializadores
        public ObservableCollection<FacturaCompraModel> FacturaCompraInfoCollection
        {
            get { return facturaCompra; }
            set { facturaCompra = value; }
        }
        #endregion

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

        public object OldFacturaCompra
        {
            get => _oldFacturaCompra;
            set
            {
                if (_oldFacturaCompra != value)
                {
                    _oldFacturaCompra = value;
                }
            }
        }

        public int FolioFactCompraIngresado
        {
            get => _folioFactCompraIngresadoText;
            set
            {
                if (_folioFactCompraIngresadoText != value)
                {
                    _folioFactCompraIngresadoText = value;
                    OnPropertyChanged(nameof(FolioFactCompraIngresado));
                }
            }
        }

        public int EditFolioFactCompra
        {
            get => _editFolioFactCompra;
            set
            {
                if (_editFolioFactCompra != value)
                {
                    _editFolioFactCompra = value;
                    OnPropertyChanged(nameof(EditFolioFactCompra));
                }
            }
        }

        public string ProveedorFactCompraIngresado
        {
            get => _proveedorFactCompraIngresadoText;
            set
            {
                if (_proveedorFactCompraIngresadoText != value)
                {
                    _proveedorFactCompraIngresadoText = value;
                    OnPropertyChanged(nameof(ProveedorFactCompraIngresado));
                }
            }
        }

        public string EditProveedorFactCompra
        {
            get => _editProveedorFactCompra;
            set
            {
                if (_editProveedorFactCompra != value)
                {
                    _editProveedorFactCompra = value;
                    OnPropertyChanged(nameof(EditProveedorFactCompra));
                }
            }
        }

        public DateTime FechaFactCompraIngresado
        {
            get => _fechaFactCompraIngresadoText;
            set
            {
                if (_fechaFactCompraIngresadoText != value)
                {
                    _fechaFactCompraIngresadoText = value;
                    OnPropertyChanged(nameof(FechaFactCompraIngresado));
                }
            }
        }

        public DateTime EditFechaFactCompra
        {
            get => _editFechaFactCompra;
            set
            {
                if (_editFechaFactCompra != value)
                {
                    _editFechaFactCompra = value;
                    OnPropertyChanged(nameof(EditFechaFactCompra));
                }
            }
        }

        public string MonedaFactCompraIngresado
        {
            get => _monedaFactCompraIngresadoText;
            set
            {
                if (_monedaFactCompraIngresadoText != value)
                {
                    _monedaFactCompraIngresadoText = value;
                    OnPropertyChanged(nameof(MonedaFactCompraIngresado));
                }
            }
        }

        public string EditMonedaFactCompra
        {
            get => _editMonedaFactCompra;
            set
            {
                if (_editMonedaFactCompra != value)
                {
                    _editMonedaFactCompra = value;
                    OnPropertyChanged(nameof(EditMonedaFactCompra));
                }
            }
        }

        #region Constructores
        public FacturaCompraViewModel(INavigationService navigationService)
        {
            _facturaCompraService = new FacturaCompraService();
            facturaCompra = new ObservableCollection<FacturaCompraModel>();

            _navigationService = navigationService;
            CargarFacturaCompra();

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
                if (item is FacturaCompraModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Folio.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Proveedor.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Fecha.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Moneda.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        // Método que se ejecuta cuando se toca una celda
        private void CeldaTocada(DataGridCellTappedEventArgs e)
        {
            if (e.RowData is FacturaCompraModel facturaCompraModel)
            {
                FolioCeldaSeleccionada = facturaCompraModel.Folio;
                ProveedorFactCompraCeldaSeleccionada = facturaCompraModel.Proveedor;
                FechaFactCompraCeldaSeleccionada = facturaCompraModel.Fecha;
                MonedaFactCompraCeldaSeleccionada = facturaCompraModel.Moneda;
                // Aquí puedes manejar la lógica de negocio sin tocar la vista
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
            CargarFacturaCompra();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    EliminarFacturaCompra(FolioCeldaSeleccionada);
                    CargarFacturaCompra();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
            }
        }

        [RelayCommand]
        public async void Agregar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Factura_Compras");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
            }
        }

        [RelayCommand]
        public async void InsertarFacturaCompra()
        {
            if (FolioFactCompraIngresado != 0 && !string.IsNullOrEmpty(ProveedorFactCompraIngresado) && !string.IsNullOrEmpty(MonedaFactCompraIngresado))
            {
                AgregarFacturaCompra(new FacturaCompraModel(FolioFactCompraIngresado, ProveedorFactCompraIngresado, FechaFactCompraIngresado, MonedaFactCompraIngresado));
                Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente", "Ok");
                _navigationService.GoBackAsync();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
            }
        }

        [RelayCommand]
        private async void Editar()
        {
            if (selectedIndex >= 0)
            {
                try
                {
                    OldFacturaCompra = new FacturaCompraModel(FolioCeldaSeleccionada, ProveedorFactCompraCeldaSeleccionada
                        , FechaFactCompraCeldaSeleccionada, MonedaFactCompraCeldaSeleccionada)
                    {
                        Folio = FolioCeldaSeleccionada,
                        Proveedor = ProveedorFactCompraCeldaSeleccionada ,
                        Fecha = FechaFactCompraCeldaSeleccionada,
                        Moneda = MonedaFactCompraCeldaSeleccionada
                    };

                    await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Factura_Compra", OldFacturaCompra);
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

        private async Task CargarFacturaCompra()
        {
            var factCompras = await _facturaCompraService.GetFacturaCompraAsync();
            facturaCompra.Clear();
            foreach (var factura in factCompras)
            {
                facturaCompra.Add(factura);
            }
        }

        private async Task AgregarFacturaCompra(FacturaCompraModel facturaCompraModel)
        {
            if (await _facturaCompraService.AddFacturaCompraAsync(facturaCompraModel))
            {
                facturaCompra.Add(facturaCompraModel);
            }
        }

        private async Task EliminarFacturaCompra(int folio)
        {
            if (await _facturaCompraService.DeleteFacturaCompraAsync(folio))
            {
                var factCompra = facturaCompra.FirstOrDefault(p => p.Folio == folio);
                if (factCompra != null)
                {
                    facturaCompra.Remove(factCompra);
                }
            }
        }

        [RelayCommand]
        public void Update()
        {
            ActualizarFacturaCompra((FacturaCompraModel)OldFacturaCompra);
            Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");
            _navigationService.GoBackAsync();
        }


        private async Task ActualizarFacturaCompra(FacturaCompraModel AntiguoFacturaCompra)
        {
            Console.WriteLine("EditFolioFactCompra: " + EditFolioFactCompra);
            Console.WriteLine("EditProveedorFactCompra: " + EditProveedorFactCompra);
            Console.WriteLine("EditFechaFactCompra: " + EditFechaFactCompra);
            Console.WriteLine("EditMonedaFactCompra: " + EditMonedaFactCompra);

            var folio = EditFolioFactCompra;
            var proveedor = EditProveedorFactCompra;
            var fecha = EditFechaFactCompra;
            var moneda = EditMonedaFactCompra;

            NewFacturaCompra = new FacturaCompraModel(folio, proveedor, fecha, moneda)
            {
                Folio = folio,
                Proveedor = proveedor,
                Fecha = fecha,
                Moneda = moneda
            };

            if (await _facturaCompraService.UpdateFacturaCompraAsync((FacturaCompraModel)NewFacturaCompra))
            {
                //Remove Old Product
                facturaCompra.Remove(AntiguoFacturaCompra);

                //Add new product
                facturaCompra.Add((FacturaCompraModel)NewFacturaCompra);

            }
        }
    }
}
