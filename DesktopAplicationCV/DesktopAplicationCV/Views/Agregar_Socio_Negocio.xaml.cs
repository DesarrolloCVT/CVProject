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
        //ViewModel.OrderInfoCollection.Insert(ViewModel.OrderInfoCollection.Count, new OrderInfo(100, "Chile", "CLP", 1050));
        //ViewModel.OrderInfoCollection.Insert(ViewModel.OrderInfoCollection.Count - 1, new OrderInfo(int.Parse(edtCodigo.Text), edtNombre.Text, "CLP", 10));
    }
}