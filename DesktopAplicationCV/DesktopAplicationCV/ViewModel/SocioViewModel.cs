using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAplicationCV.Models
{
    public partial class SocioViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<OrderInfo> orderInfo;

        /*public ObservableCollection<OrderInfo> OrderInfoCollection
        {
            get { return orderInfo; }
            set { orderInfo = value; }
        }*/

        public SocioViewModel()
        {
            orderInfo = new ObservableCollection<OrderInfo>();
            GenerateOrders();
        }

        public void GenerateOrders()
        {
            orderInfo.Add(new OrderInfo(0, "Germany", "ALFKI", 10));
            orderInfo.Add(new OrderInfo(1, "Mexico", "ANATR", 10));
            orderInfo.Add(new OrderInfo(2,"Mexico", "ANTON", 10));
            orderInfo.Add(new OrderInfo(3,"UK", "AROUT", 10));
            orderInfo.Add(new OrderInfo(4,"Sweden", "BERGS", 10));
            orderInfo.Add(new OrderInfo(5,"Germany", "BLAUS", 10));
            orderInfo.Add(new OrderInfo(6,"France", "BLONP", 10));
            orderInfo.Add(new OrderInfo(7,"Spain", "BOLID", 10));
            orderInfo.Add(new OrderInfo(8,"France", "BONAP", 10));
            orderInfo.Add(new OrderInfo(9,"Canada", "BOTTM", 10));
            orderInfo.Add(new OrderInfo(10,"UK", "AROUT", 10));
            orderInfo.Add(new OrderInfo(11,"Germany", "BLAUS", 10));
            orderInfo.Add(new OrderInfo(12,"France", "BLONP", 10));
            orderInfo.Add(new OrderInfo(13,"UK", "AROUT", 10));
            orderInfo.Add(new OrderInfo(14, "CL", "TANGANANA", 1050));
            orderInfo.Add(new OrderInfo(15, "CL", "TANGANANICA", 3550));
        }
    }
}
