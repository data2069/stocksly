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
        public decimal Total { get; set; }

        #region Helper Attributes

        public List<PurchaseOrderItem> PurchaseOrderItems { get; set; }

        #endregion

        public PurchaseOrder()
        {
            PurchaseOrderItems = new List<PurchaseOrderItem>();
        }
    }
}
