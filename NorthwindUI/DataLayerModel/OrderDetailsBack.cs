using System;
using System.Collections.Generic;

//#nullable disable

namespace NorthwindUI.DataLayerModel
{
    public partial class OrderDetailsBack
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public virtual OrdersBack Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
