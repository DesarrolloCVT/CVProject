﻿using CommunityToolkit.Mvvm.ComponentModel;
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

        private static object _oldType;
        private object NewType;

        private int CodigoCeldaSeleccionada;
        private string NombreTipoCeldaSeleccionada;
        private string TipoCeldaSeleccionada;
        private string CuentaTipoCeldaSeleccionada;
        private int PagoFacturaTipoCeldaSeleccionada;
        private int GastoComercializacionTipoCeldaSeleccionada;
        private int ComisionesTipoCeldaSeleccionada;
        private int GastoFinancieroTipoCeldaSeleccionada;
        private int AnticipoTipoCeldaSeleccionada;

        private readonly INavigationService _navigationService;
        private readonly TipoService _tipoService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<TipoModel> Tipos;

        private string _filterText;

        private int _codigoTipoIngresadoText;
        private string _nombreTipoIngresadoText;
        private string _tipoIngresadoText;
        private string _cuentaTipoIngresadoText;
        private int _pagoFacturaTipoIngresadoText;
        private int _gastoComercializacionTipoIngresadoText;
        private int _comisionesTipoIngresadoText;
        private int _gastoFinancieroTipoIngresadoText;
        private int _anticipoTipoIngresadoText;

        private int _editCodigoTipo;
        private string _editNombreTipo;
        private string _editTipo;
        private string _editCuentaTipo;
        private int _editPagoFacturaTipo;
        private int _editGastoComercializacionTipo;
        private int _editComisionesTipo;
        private int _editGastoFinancieroTipo;
        private int _editAnticipoTipo;

        public ICommand CargarTiposCommand { get; }
        public ICommand AgregarTipoCommand { get; }
        public ICommand EliminarTipoCommand { get; }
        public ICommand ActualizarTipoCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Inicializadores
        public ObservableCollection<TipoModel> TipoInfoCollection
        {
            get { return Tipos; }
            set { Tipos = value; }
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

        public string CuentaTipoIngresado
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

        public string EditCuentaTipo
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

        public int EditPagoFacturaTipo
        {
            get => _editPagoFacturaTipo;
            set
            {
                if (_editPagoFacturaTipo != value)
                {
                    _editPagoFacturaTipo = value;
                    OnPropertyChanged(nameof(EditPagoFacturaTipo));
                }
            }
        }

        public int EditGastoComercializacionTipo
        {
            get => _editGastoComercializacionTipo;
            set
            {
                if (_editGastoComercializacionTipo != value)
                {
                    _editGastoComercializacionTipo = value;
                    OnPropertyChanged(nameof(EditGastoComercializacionTipo));
                }
            }
        }

        public int EditComisionesTipo
        {
            get => _editComisionesTipo;
            set
            {
                if (_editComisionesTipo != value)
                {
                    _editComisionesTipo = value;
                    OnPropertyChanged(nameof(EditComisionesTipo));
                }
            }
        }

        public int EditGastoFinancieroTipo
        {
            get => _editGastoFinancieroTipo;
            set
            {
                if (_editGastoFinancieroTipo != value)
                {
                    _editGastoFinancieroTipo = value;
                    OnPropertyChanged(nameof(EditGastoFinancieroTipo));
                }
            }
        }

        public int EditAnticipoTipo
        {
            get => _editAnticipoTipo;
            set
            {
                if (_editAnticipoTipo != value)
                {
                    _editAnticipoTipo = value;
                    OnPropertyChanged(nameof(EditAnticipoTipo));
                }
            }
        }

        public int PagoFacturaTipoIngresado
        {
            get => _pagoFacturaTipoIngresadoText;
            set
            {
                if (_pagoFacturaTipoIngresadoText != value)
                {
                    _pagoFacturaTipoIngresadoText = value;
                    OnPropertyChanged(nameof(PagoFacturaTipoIngresado));
                }
            }
        }
        
        public int GastoComercializacionTipoIngresado
        {
            get => _gastoComercializacionTipoIngresadoText;
            set
            {
                if (_gastoComercializacionTipoIngresadoText != value)
                {
                    _gastoComercializacionTipoIngresadoText = value;
                    OnPropertyChanged(nameof(GastoComercializacionTipoIngresado));
                }
            }
        }
        
        public int ComisionesTipoIngresado
        {
            get => _comisionesTipoIngresadoText;
            set
            {
                if (_comisionesTipoIngresadoText != value)
                {
                    _comisionesTipoIngresadoText = value;
                    OnPropertyChanged(nameof(ComisionesTipoIngresado));
                }
            }
        }
        
        public int GastoFinancieroTipoIngresado
        {
            get => _gastoFinancieroTipoIngresadoText;
            set
            {
                if (_gastoFinancieroTipoIngresadoText != value)
                {
                    _gastoFinancieroTipoIngresadoText = value;
                    OnPropertyChanged(nameof(GastoFinancieroTipoIngresado));
                }
            }
        }
        
        public int AnticipoTipoIngresado
        {
            get => _anticipoTipoIngresadoText;
            set
            {
                if (_anticipoTipoIngresadoText != value)
                {
                    _anticipoTipoIngresadoText = value;
                    OnPropertyChanged(nameof(AnticipoTipoIngresado));
                }
            }
        }

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
                           data.Cuenta.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Pago_Factura.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Gasto_Comercializacion.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Comisiones.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Gasto_Financiero.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Anticipo.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }

        // Método que se ejecuta cuando se toca una celda
        private void CeldaTocada(DataGridCellTappedEventArgs e)
        {
            if (e.RowData is TipoModel tipos)
            {
                CodigoCeldaSeleccionada = tipos.Codigo;
                NombreTipoCeldaSeleccionada = tipos.Nombre;
                TipoCeldaSeleccionada = tipos.Tipo_Dato;
                CuentaTipoCeldaSeleccionada = tipos.Cuenta;
                PagoFacturaTipoCeldaSeleccionada = tipos.Pago_Factura;
                GastoComercializacionTipoCeldaSeleccionada = tipos.Gasto_Comercializacion;
                ComisionesTipoCeldaSeleccionada = tipos.Comisiones;
                GastoFinancieroTipoCeldaSeleccionada = tipos.Gasto_Financiero;
                AnticipoTipoCeldaSeleccionada = tipos.Anticipo;
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
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Tipo");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
            }
        }

        [RelayCommand]
        public async void InsertarTipo()
        {
            if (CodigoTipoIngresado != 0 && !string.IsNullOrEmpty(NombreTipoIngresado) 
                && !string.IsNullOrEmpty(TipoIngresado) && !string.IsNullOrEmpty(CuentaTipoIngresado) 
                && PagoFacturaTipoIngresado != 0 && GastoComercializacionTipoIngresado != 0 
                && ComisionesTipoIngresado != 0 && GastoFinancieroTipoIngresado != 0 && AnticipoTipoIngresado != 0)
            {
                AgregarTipo(new TipoModel(CodigoTipoIngresado, NombreTipoIngresado, TipoIngresado, 
                    CuentaTipoIngresado, PagoFacturaTipoIngresado, GastoComercializacionTipoIngresado, ComisionesTipoIngresado
                    , GastoFinancieroTipoIngresado, AnticipoTipoIngresado));
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
                    OldType = new TipoModel(CodigoCeldaSeleccionada, NombreTipoCeldaSeleccionada,
                        TipoCeldaSeleccionada, CuentaTipoCeldaSeleccionada, PagoFacturaTipoCeldaSeleccionada
                        , GastoComercializacionTipoCeldaSeleccionada, ComisionesTipoCeldaSeleccionada, GastoFinancieroTipoCeldaSeleccionada
                        ,AnticipoTipoCeldaSeleccionada)
                    {
                        Codigo = CodigoCeldaSeleccionada,
                        Nombre = NombreTipoCeldaSeleccionada,
                        Tipo_Dato = TipoCeldaSeleccionada,
                        Cuenta = CuentaTipoCeldaSeleccionada,
                        Pago_Factura = PagoFacturaTipoCeldaSeleccionada,
                        Gasto_Comercializacion = GastoComercializacionTipoCeldaSeleccionada,
                        Comisiones = ComisionesTipoCeldaSeleccionada,
                        Gasto_Financiero = GastoFinancieroTipoCeldaSeleccionada,
                        Anticipo = AnticipoTipoCeldaSeleccionada
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

        private async Task CargarTipos()
        {
            var tipos = await _tipoService.GetTipoAsync();
            Tipos.Clear();
            foreach (var tipo in tipos)
            {
                Tipos.Add(tipo);
            }
        }

        private async Task AgregarTipo(TipoModel tipo)
        {
            if (await _tipoService.AddTipoAsync(tipo))
            {
                Tipos.Add(tipo);
            }
        }

        private async Task EliminarTipo(int codigo)
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

        [RelayCommand]
        public void Update()
        {
            ActualizarTipo((TipoModel)OldType);
            Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");
            _navigationService.GoBackAsync();
        }


        private async Task ActualizarTipo(TipoModel AntiguoProducto)
        {
            Console.WriteLine("EditCodigoTipo: " + EditCodigoTipo);
            Console.WriteLine("EditNombreTipo: " + EditNombreTipo);
            Console.WriteLine("EditTipo: " + EditTipo);
            Console.WriteLine("EditCuentaTipo: " + EditCuentaTipo);

            var cod = EditCodigoTipo;
            var name = EditNombreTipo;
            var type = EditTipo;
            var account = EditCuentaTipo;
            var pagoFactura = EditPagoFacturaTipo;
            var gastoComercializacion = EditGastoComercializacionTipo;
            var comisiones = EditComisionesTipo;
            var gastoFinanciero = EditGastoFinancieroTipo;
            var anticipo = EditAnticipoTipo;


            NewType = new TipoModel(cod, name, type, account, pagoFactura, gastoComercializacion, comisiones, gastoFinanciero, anticipo)
            {
                Codigo = cod,
                Nombre = name,
                Tipo_Dato = type,
                Cuenta = account,
                Pago_Factura = pagoFactura,
                Gasto_Comercializacion = gastoComercializacion,
                Comisiones = comisiones,
                Gasto_Financiero = gastoFinanciero,
                Anticipo = anticipo
            };

            if (await _tipoService.UpdateTipoAsync((TipoModel)NewType))
            {
                //Remove Old Product
                Tipos.Remove(AntiguoProducto);

                //Add new product
                Tipos.Add((TipoModel)NewType);

            }
        }
    }
}