using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevERP.Model
{
    public class StockData
    {
        public double PartsCode { get; set; }
        public string PartsName { get; set; }

        public double PartsQty { get; set; }

        public string Unit { get; set; }

        public double Rate { get; set; }
        public double Total { get; set; }
        public string Department { get; set; }
        public string CompanyName { get; set; }
    }
}