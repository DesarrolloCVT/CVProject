using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Maui.Animations;
using System.Security.Cryptography;

namespace DesktopAplicationCV.Views;

public partial class Login : ContentPage
{
    public Login()
	{
		InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        //Agregar validacion para el inicio de sesion.
        await Navigation.PushAsync(new MenuPrincipal());
    }

    private void HashPassword(string clave)
    {
        string? password = clave;

        // Generate a 128-bit salt using a sequence of
        // cryptographically strong random bytes.
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
        Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: clave!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        txtUsuario.Text = hashed;
    }

    private void txtPassword_Completed(object sender, EventArgs e)
    {
        HashPassword(txtPassword.Text);
    }
}