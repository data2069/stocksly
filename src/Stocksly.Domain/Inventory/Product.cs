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
        public bool Discontinued { get; set; }

        #region Associations

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public Supplier Supplier { get; set; }
        public int SupplierId { get; set; }

        #endregion
    }
}
