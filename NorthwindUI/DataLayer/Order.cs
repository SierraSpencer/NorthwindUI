using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindUI
{
    public class Order
    {
        public int orderId { get; set; }
        public string customerId { get; set; }
        public int employeeId { get; set; }
        public DateTime? orderDate { get; set; }
        public DateTime? requiredDate { get; set; }
        public DateTime? shippedDate { get; set; }
        public int shipVia { get; set; }
        public float freight { get; set; }
        public string shipName { get; set; }
        public string shipAddress { get; set; }
        public string shipCity { get; set; }
        public object shipRegion { get; set; }
        public string shipPostalCode { get; set; }
        public string shipCountry { get; set; }
        public object customer { get; set; }
        public object employee { get; set; }
        public object shipViaNavigation { get; set; }
        public object[] orderDetails { get; set; }
    }
}
