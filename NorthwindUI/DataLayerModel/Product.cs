using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindUI
{
    public class Product
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public int supplierId { get; set; }
        public int categoryId { get; set; }
        public string quantityPerUnit { get; set; }
        public float unitPrice { get; set; }
        public int unitsInStock { get; set; }
        public int unitsOnOrder { get; set; }
        public int reorderLevel { get; set; }
        public bool discontinued { get; set; }
        public object category { get; set; }
        public object supplier { get; set; }
        public object[] orderDetails { get; set; }
        public object[] orderDetailsBacks { get; set; }
    }
}
