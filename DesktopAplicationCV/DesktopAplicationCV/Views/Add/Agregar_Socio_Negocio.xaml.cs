using DesktopAplicationCV.Models;
using Syncfusion.Maui.Core.Carousel;
using System.Collections.ObjectModel;

namespace DesktopAplicationCV.Views;

public partial class Agregar_Socio_Negocio : ContentPage
{
    public Agregar_Socio_Negocio()
	{
		InitializeComponent();
    }

    private void InsertarButton_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Alerta", "Se registra nuevo dato en la BD", "OK");
        //ViewModel.SocioInfoCollection.Insert(ViewModel.SocioInfoCollection.Count, new SocioNegocioModel(100, "Chile", "CLP", 1050));
        //ViewModel.SocioInfoCollection.Insert(ViewModel.SocioInfoCollection.Count - 1, new SocioNegocioModel(int.Parse(edtCodigo.Text), edtNombre.Text, "CLP", 10));
    }
}