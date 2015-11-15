using Stocksly.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocksly.Domain.Sales
{
    public class SalesOrder
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal Total { get; set; }
        public int CustomerId { get; set; }

        #region Helper Attributes

        public Customer Customer { get; set; }
        public List<SalesOrderItem> SalesOrderItems { get; set; }

        #endregion

        public SalesOrder()
        {
            SalesOrderItems = new List<SalesOrderItem>();
        }
    }
}
