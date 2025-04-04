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
    public partial class SubtiposViewModel : BaseViewModel
    {
        #region Variables
        private static int IdSubtipoCeldaSeleccionada;
        private string IdentificadorSubtipoCeldaSeleccionada;
        private string NombreSubtipoCeldaSeleccionada;

        private static object _oldSubtype;
        private object NewSubtype;

        private readonly INavigationService _navigationService;
        private readonly SubtiposService _subTiposService;

        [ObservableProperty]
        private int selectedIndex;

        private ObservableCollection<SubtiposModel> Subtipo;

        private string _filterText;
        private string _identificadorSubTipoIngresadoText;
        private string _nombreSubTipoIngresadoText;

        private string _editIdentificadorSubTipo;
        private string _editNombreSubTipo;

        public ICommand CargarSubTipoCommand { get; }
        public ICommand AgregarSubTipoCommand { get; }
        public ICommand EliminarSubTipoCommand { get; }
        public ICommand ActualizarSubTipoCommand { get; }
        public ICommand CeldaTocadaCommand { get; }

        #endregion

        #region Encapsulado
        public ObservableCollection<SubtiposModel> SubtiposInfoCollection
        {
            get { return Subtipo; }
            set { Subtipo = value; }
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

        public object OldSubtype
        {
            get => _oldSubtype;
            set
            {
                if (_oldSubtype != value)
                {
                    _oldSubtype = value;
                }
            }
        }

        public string IdentificadorSubTipoIngresadoText
        {
            get => _identificadorSubTipoIngresadoText;
            set
            {
                if (_identificadorSubTipoIngresadoText != value)
                {
                    _identificadorSubTipoIngresadoText = value;
                    OnPropertyChanged(nameof(IdentificadorSubTipoIngresadoText));
                }
            }
        }

        public string NombreSubTipoIngresadoText
        {
            get => _nombreSubTipoIngresadoText;
            set
            {
                if (_nombreSubTipoIngresadoText != value)
                {
                    _nombreSubTipoIngresadoText = value;
                    OnPropertyChanged(nameof(NombreSubTipoIngresadoText));
                }
            }
        }

        public string EditIdentificadorSubTipo
        {
            get => _editIdentificadorSubTipo;
            set
            {
                if (_editIdentificadorSubTipo != value)
                {
                    _editIdentificadorSubTipo = value;
                    OnPropertyChanged(nameof(EditIdentificadorSubTipo));
                }
            }
        }

        public string EditNombreSubTipo
        {
            get => _editNombreSubTipo;
            set
            {
                if (_editNombreSubTipo != value)
                {
                    _editNombreSubTipo = value;
                    OnPropertyChanged(nameof(EditNombreSubTipo));
                }
            }
        }

        // Acción para establecer la lógica del filtro
        public Action ApplyFilterAction { get; set; }
        #endregion

        #region Constructores
        public SubtiposViewModel(INavigationService navigationService)
        {
            _subTiposService = new SubtiposService();
            Subtipo = new ObservableCollection<SubtiposModel>();
            _navigationService = navigationService;

            CargarSubTipos();

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
                if (e.RowData is SubtiposModel subTipo)
                {
                    IdSubtipoCeldaSeleccionada = subTipo.Id_Subtipos;
                    IdentificadorSubtipoCeldaSeleccionada = subTipo.Identificador;
                    NombreSubtipoCeldaSeleccionada = subTipo.Nombre;
                    // Aquí puedes manejar la lógica de negocio sin tocar la vista
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error CeldaTocada SubTipoViewModel: " + ex.Message);
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
            CargarSubTipos();
        }

        [RelayCommand]
        public void Eliminar()
        {
            try
            {
                if (selectedIndex >= 0)
                {
                    EliminarSubTipo(IdSubtipoCeldaSeleccionada);
                    CargarSubTipos();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error en la seleccion de la fila a eliminar ", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
                Console.WriteLine("Error Eliminar SubTipoViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void Agregar()
        {
            try
            {
                await _navigationService.NavigateToAsync<NavigationViewModel>("Agregar_Subtipo");
            }
            catch (Exception Ex)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Error: " + Ex.Message, "Ok");
                Console.WriteLine("Error Agregar SubTipoViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public async void InsertarSubTipo()
        {
            try
            {
                if (!string.IsNullOrEmpty(IdentificadorSubTipoIngresadoText) && !string.IsNullOrEmpty(NombreSubTipoIngresadoText))
                {
                    AgregarSubTipo(new SubtiposModel(0, IdentificadorSubTipoIngresadoText, NombreSubTipoIngresadoText));
                    _navigationService.GoBackAsync();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error durante la insercion", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error InsertarMoneda SubTipoViewModel: " + Ex.Message);
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
                        OldSubtype = new SubtiposModel(IdSubtipoCeldaSeleccionada, IdentificadorSubtipoCeldaSeleccionada, NombreSubtipoCeldaSeleccionada)
                        {
                            Identificador = IdentificadorSubtipoCeldaSeleccionada,
                            Nombre = NombreSubtipoCeldaSeleccionada
                        };

                        await _navigationService.NavigateToAsync<NavigationViewModel>("Editar_Subtipos", OldSubtype);
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
                Console.WriteLine("Error Editar SubTipoViewModel: " + Ex.Message);
            }
        }

        private async Task CargarSubTipos()
        {
            try
            {
                Console.WriteLine("SubTiposInfoCollection: " + SubtiposInfoCollection);
                var subTipo = await _subTiposService.GetSubtiposAsync();
                Subtipo.Clear();
                foreach (var subTipos in subTipo)
                {
                    Subtipo.Add(subTipos);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Editar SubTipoViewModel: " + Ex.Message);
            }
        }

        private async Task AgregarSubTipo(SubtiposModel subTipo)
        {
            try
            {
                if (await _subTiposService.AddSubTipoAsync(subTipo))
                {
                    Subtipo.Add(subTipo);
                    Application.Current.MainPage.DisplayAlert("Alerta", "Datos insertados correctamente. ", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alerta", "Se ha producido un error, ya existe una entrada asignada con este Codigo.", "Ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error AgregarMoneda SubTipoViewModel: " + Ex.Message);
            }
        }

        private async Task EliminarSubTipo(int id)
        {
            try
            {
                if (await _subTiposService.DeleteSubTipoAsync(id))
                {
                    var subTipo = Subtipo.FirstOrDefault(p => p.Id_Subtipos == id);
                    if (subTipo != null)
                    {
                        Subtipo.Remove(subTipo);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error EliminarMoneda SubTipoViewModel: " + Ex.Message);
            }
        }

        [RelayCommand]
        public void Update()
        {
            try
            {
                ActualizarSubTipo((SubtiposModel)OldSubtype);
                Application.Current.MainPage.DisplayAlert("Alerta", "Datos actualizados correctamente", "Ok");
                _navigationService.GoBackAsync();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error Update SubTipoViewModel: " + Ex.Message);
            }
        }

        private async Task ActualizarSubTipo(SubtiposModel AntiguoSubTipo)
        {
            try
            {
                Console.WriteLine("EditNombreSubTipo: " + EditNombreSubTipo);

                var id = IdSubtipoCeldaSeleccionada;
                var identify = EditIdentificadorSubTipo;
                var name = EditNombreSubTipo;

                NewSubtype = new SubtiposModel(id, identify, name)
                {
                    Id_Subtipos = id,
                    Identificador = identify,
                    Nombre = name
                };

                if (await _subTiposService.UpdateSubTipoAsync((SubtiposModel)NewSubtype))
                {
                    //Remove Old Product
                    Subtipo.Remove(AntiguoSubTipo);

                    //Add new product
                    Subtipo.Add((SubtiposModel)NewSubtype);

                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error ActualizarMoneda SubTipoViewModel: " + Ex.Message);
            }
        }

        // Lógica de filtrado como delegado
        public Predicate<object> GetFilter()
        {
            return item =>
            {
                if (item is SubtiposModel data)
                {
                    return string.IsNullOrWhiteSpace(FilterText) ||
                           data.Id_Subtipos.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Identificador.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                           data.Nombre.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            };
        }
        #endregion
    }
}
