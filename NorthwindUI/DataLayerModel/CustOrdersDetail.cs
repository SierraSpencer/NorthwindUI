using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindUI
{
    public class CustOrdersDetail
    {
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public double ExtendedPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public int ProductID { get; set; }
    }
}
