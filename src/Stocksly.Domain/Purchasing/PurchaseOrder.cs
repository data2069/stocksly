using Stocksly.Domain.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocksly.Domain.Purchasing
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public int Count { get; set; }
        public decimal Total { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        #region Helper Attributes

        public List<PurchaseOrderItem> OrderItems { get; set; }
        //public Supplier Supplier { get; set; }

        #endregion

        public PurchaseOrder()
        {
            OrderItems = new List<PurchaseOrderItem>();
            OrderTime = DateTime.Now;
        }
    }
}
