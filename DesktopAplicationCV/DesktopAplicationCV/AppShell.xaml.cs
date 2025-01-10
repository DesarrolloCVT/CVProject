using DesktopAplicationCV.Views;

namespace DesktopAplicationCV
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("MenuPrincipal", typeof(MenuPrincipal));
        }
    }
}
