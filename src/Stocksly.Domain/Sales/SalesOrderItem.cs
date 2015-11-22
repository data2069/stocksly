using Stocksly.Domain.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocksly.Domain.Sales
{
    public class SalesOrderItem
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public int Quantity { get; set; }
        public int StocksRemainings { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }

        #region Associations

        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string ProductCode { get; set; }
        public string ProductDisplayName { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        #endregion
    }
}
