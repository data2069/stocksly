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
        public int ReorderQuantity { get; set; }
        public int ReorderLevel { get; set; }
        public int Stocks { get; set; }

        #region Helper Attributes

        public Supplier Supplier { get; set; }
        public Category Category { get; set; }

        #endregion
    }
}
