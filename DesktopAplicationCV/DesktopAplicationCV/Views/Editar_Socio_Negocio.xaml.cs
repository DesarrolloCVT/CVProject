namespace DesktopAplicationCV.Views;

public partial class Editar_Socio_Negocio : ContentPage
{
	private int codigo;
	private string nombre;

	public Editar_Socio_Negocio(int cod, string name)
	{
		InitializeComponent();
		codigo = cod;
		nombre = name;
		Data();
	}

	public void Data()
	{
		edtCodigo.Text = codigo.ToString();
		edtNombre.Text = nombre;
		edtCodigo.Focus();
	}

    private void ModificarButton_Clicked(object sender, EventArgs e)
    {
		DisplayAlert("Alerta", "Se modifican los datos en la BD", "OK");
    }
}