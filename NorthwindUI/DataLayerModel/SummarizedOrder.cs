﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindUI.DataLayerModel
{
    public class SummarizedOrder
    {
        public int OrderId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
