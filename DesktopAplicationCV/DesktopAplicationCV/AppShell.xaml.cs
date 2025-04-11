using DesktopAplicationCV.Views;

namespace DesktopAplicationCV
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("MenuPrincipal", typeof(MenuPrincipal));
            Routing.RegisterRoute("LoginPage", typeof(Login));
            Routing.RegisterRoute("Banco", typeof(Banco));
            Routing.RegisterRoute("Banco_Detalle", typeof(Banco_Detalle));
            Routing.RegisterRoute("Compra_Venta", typeof(Compra_Venta));
            Routing.RegisterRoute("Cuentas", typeof(Cuentas));
            Routing.RegisterRoute("Factura_Compra", typeof(Factura_Compra));
            Routing.RegisterRoute("Factura_Compra_Detalle", typeof(Factura_Compra_Detalle));
            Routing.RegisterRoute("Factura_Venta", typeof(Factura_Venta));
            Routing.RegisterRoute("Factura_Venta_Detalle", typeof(Factura_Venta_Detalle));
            Routing.RegisterRoute("Ingresos", typeof(Ingresos));
            Routing.RegisterRoute("Ingresos_Detalle", typeof(Ingresos_Detalle));
            Routing.RegisterRoute("Productos", typeof(Productos));
            Routing.RegisterRoute("Socio_Negocio", typeof(Socio_Negocio));
            Routing.RegisterRoute("Tipo", typeof(Tipo));
        }
    }
}
