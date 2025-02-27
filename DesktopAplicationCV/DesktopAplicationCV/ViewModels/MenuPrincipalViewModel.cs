using CommunityToolkit.Mvvm.ComponentModel;
using DesktopAplicationCV.Views;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.ViewModels
{
    public partial class MenuPrincipalViewModel : ObservableObject
    {
        public void AgregarTabs(TabbedPage tabbedPage, LoginViewModel loginViewModel)
        {
            tabbedPage.Children.Add(new Inicio(loginViewModel)
            {
                Title = "Inicio"
            });

            tabbedPage.Children.Add(new Operaciones_Bancarias(loginViewModel)
            {
                Title = "Operaciones Bancarias"
                
            });

            tabbedPage.Children.Add(new Maestros(loginViewModel)
            {
                Title = "Maestros"
            });

            tabbedPage.Children.Add(new Compra_Venta(loginViewModel)
            {
                Title = "Compra Venta"
            });
        }
    }
}