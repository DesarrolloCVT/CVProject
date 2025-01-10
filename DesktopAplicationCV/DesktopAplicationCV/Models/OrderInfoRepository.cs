using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImageSource = Microsoft.Maui.Controls.ImageSource;

namespace DesktopAplicationCV.Models
{
    public class OrderInfoRepository
    {
        private ObservableCollection<OrderInfo> orderInfo;
        public ObservableCollection<OrderInfo> OrderInfoCollection
        {
            get { return orderInfo; }
            set { orderInfo = value; }
        }

        public OrderInfoRepository()
        {
            orderInfo = new ObservableCollection<OrderInfo>();
            GenerateOrders();
        }

        public void GenerateOrders()
        {
            orderInfo.Add(new OrderInfo(ImageSource.FromFile("eliminar.png"), ImageSource.FromFile("editar.png"), "Germany", "ALFKI", "Berlin", 10));
            orderInfo.Add(new OrderInfo(ImageSource.FromFile("eliminar.png"), ImageSource.FromFile("editar.png"), "Mexico", "ANATR", "Mexico D.F.", 10));
            orderInfo.Add(new OrderInfo(ImageSource.FromFile("eliminar.png"), ImageSource.FromFile("editar.png"), "Mexico", "ANTON", "Mexico D.F.", 10));
            orderInfo.Add(new OrderInfo(ImageSource.FromFile("eliminar.png"), ImageSource.FromFile("editar.png"), "UK", "AROUT", "London", 10));
            orderInfo.Add(new Order Info(ImageSource.FromFile("eliminar.png"), ImageSource.FromFile("editar.png"), "Sweden", "BERGS", "London", 10));
            orderInfo.Add(new OrderInfo(ImageSource.FromFile("eliminar.png"), ImageSource.FromFile("editar.png"), "Germany", "BLAUS", "Mannheim", 10));
            orderInfo.Add(new OrderInfo(ImageSource.FromFile("eliminar.png"), ImageSource.FromFile("editar.png"), "France", "BLONP", "Strasbourg", 10));
            orderInfo.Add(new OrderInfo(ImageSource.FromFile("eliminar.png"), ImageSource.FromFile("editar.png"), "Spain", "BOLID", "Madrid", 10));
            orderInfo.Add(new OrderInfo(ImageSource.FromFile("eliminar.png"), ImageSource.FromFile("editar.png"), "France", "BONAP", "Marsiella", 10));
            orderInfo.Add(new OrderInfo(ImageSource.FromFile("eliminar.png"), ImageSource.FromFile("editar.png"), "Canada", "BOTTM", "Lenny Lin", 10));
            orderInfo.Add(new OrderInfo(ImageSource.FromFile("eliminar.png"), ImageSource.FromFile("editar.png"), "UK", "AROUT", "London", 10));
            orderInfo.Add(new OrderInfo(ImageSource.FromFile("eliminar.png"), ImageSource.FromFile("editar.png"), "Germany", "BLAUS", "Mannheim", 10));
            orderInfo.Add(new OrderInfo(ImageSource.FromFile("eliminar.png"), ImageSource.FromFile("editar.png"), "France", "BLONP", "Strasbourg", 10));
            orderInfo.Add(new OrderInfo(ImageSource.FromFile("eliminar.png"), ImageSource.FromFile("editar.png"), "UK", "AROUT", "London", 10));
        }
    }
}
