using System;
using System.Collections.Generic;

//#nullable disable

namespace NorthwindUI.DataLayerModel
{
    public partial class Shipper
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
            OrdersBacks = new HashSet<OrdersBack>();
        }

        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<OrdersBack> OrdersBacks { get; set; }
    }
}
