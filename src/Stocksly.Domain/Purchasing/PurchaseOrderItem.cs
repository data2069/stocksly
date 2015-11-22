using Stocksly.Domain.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocksly.Domain.Purchasing
{
    public class PurchaseOrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int StocksAvailable { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }

        #region Associations

        public int PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string ProductCode { get; set; }
        public string ProductDisplayName { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        #endregion
    }
}
