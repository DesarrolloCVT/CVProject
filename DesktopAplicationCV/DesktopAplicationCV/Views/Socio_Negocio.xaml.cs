namespace DesktopAplicationCV.Views;

using DesktopAplicationCV.Models;
using Syncfusion.Maui.DataGrid;

public partial class Socio_Negocio : ContentPage
{
    //private string seleccionado;
    //private int Codigo;
    //private string Nombre;

    /*public string Seleccionado
    {
        get { return seleccionado; }
        set { this.seleccionado = value; }
    }*/

    public Socio_Negocio()
	{
        InitializeComponent();

        try
        {
            BindingContext = new SocioViewModel(DependencyService.Get<INavigationService>());
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: Socio_Negocio: " + ex.Message);
        }
        //SfDataGrid dataGrid = new SfDataGrid();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        dataGrid.Refresh();
    }

    /*private void EliminarButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            var selectedItems = dataGrid.SelectedRows.ToList();
            if(selectedItems.Count > 0)
            {
                foreach (var item in selectedItems)
                {
                    ViewModel.OrderInfo.Remove((OrderInfo)item);
                }
                this.dataGrid!.ClearSelection();
            }
            else
            {
                DisplayAlert("Alerta", "Seleccione las filas que desea eliminar", "Ok");
            }
        }
        catch (Exception)
        {
            DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
        }
    }*/

    private void EditarButton_Clicked(object sender, EventArgs e)
    {   
        /*try
        {
            var selectedItems = dataGrid.SelectedRows.ToList();

            if (selectedItems.Count == 1)
            {
                Navigation.PushAsync(new Editar_Socio_Negocio(Codigo,Nombre));
            }
            else if (selectedItems.Count > 1)
            {
                DisplayAlert("Alerta", "Debe seleccionar unicamente una fila para modificar", "Ok");
                this.dataGrid!.ClearSelection();
            }
            else
            {
                DisplayAlert("Alerta", "Seleccione una fila para modificar", "Ok");
            }
        }
        catch (Exception)
        {
            DisplayAlert("Alerta", "Se ha producido un error durante el proceso", "Ok");
        }*/
    }

    private void AgregarButton_Clicked(object sender, EventArgs e)
    {
        //DisplayAlert("Alerta", "Selected.Rows - Count: " + dataGrid.SelectedRows.Count, "OK");
        //Navigation.PushAsync(new Agregar_Socio_Negocio());
    }

    private void NombreEditor_Completed(object sender, EventArgs e)
    {
        /*Seleccionado = "Nombre";
        if (!string.IsNullOrEmpty(txtNombre.Text))
        {
            dataGrid.View.Filter = FilterRecords;
        }
        else
        {
            dataGrid.View.Filter = null;
        }
        dataGrid.View.RefreshFilter();*/
    }

    private void CodigoEditor_Completed(object sender, EventArgs e)
    {
        /*Seleccionado = "Codigo";
        if (!string.IsNullOrEmpty(txtCodigo.Text))
        {
            dataGrid.View.Filter = FilterRecords;
        }
        else
        {
            dataGrid.View.Filter = null;
        }
        dataGrid.View.RefreshFilter();*/
    }

    private void TipoEditor_Completed(object sender, EventArgs e)
    {
        /*Seleccionado = "Tipo";
        if (!string.IsNullOrEmpty(txtTipo.Text))
        {
            dataGrid.View.Filter = FilterRecords;
        }
        else
        {
            dataGrid.View.Filter = null;
        }
        dataGrid.View.RefreshFilter();*/
    }

    private void SaldoEditor_Completed(object sender, EventArgs e)
    {
        /*Seleccionado = "Saldo";
        if (!string.IsNullOrEmpty(txtSaldo.Text))
        {
            dataGrid.View.Filter = FilterRecords;
        }
        else
        {
            dataGrid.View.Filter = null;
        }
        dataGrid.View.RefreshFilter();*/
    }

    /*private bool FilterRecords(object record)
    {
        var orderInfo = record as OrderInfo;
        switch (seleccionado)
        {
            case "Nombre":
                if (orderInfo != null && orderInfo.Nombre.ToLower() == txtNombre.Text.ToLower())
                {
                    return true;
                }
                return false;
            case "Codigo":
                try
                {
                    if (orderInfo != null && orderInfo.Codigo == int.Parse(txtCodigo.Text))
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
                
            case "Tipo":
                if (orderInfo != null && orderInfo.Tipo.ToLower() == txtTipo.Text.ToLower())
                {
                    return true;
                }
                return false;
            default:
                try
                {
                    if (orderInfo != null && orderInfo.Saldo == int.Parse(txtSaldo.Text))
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception) 
                {
                    return false;
                }
        }
    }*/

    private void dataGrid_CellTapped(object sender, DataGridCellTappedEventArgs e)
    {
        /*try
        {
            var empdata = (OrderInfo)e.RowData;
            if (empdata != null) 
            {
                Codigo = empdata.Codigo;
                Nombre = empdata.Nombre;
            }
        }
        catch (Exception) 
        {

        }*/
    }
}