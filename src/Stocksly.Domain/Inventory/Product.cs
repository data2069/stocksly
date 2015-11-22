using Stocksly.Domain.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocksly.Domain.Inventory
{
    public class Product : ProductBrief
    {
        public int ReorderLevel { get; set; }
        public int Stocks { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Discontinued { get; set; }

        #region Helper Attributes

        #endregion
    }
}
