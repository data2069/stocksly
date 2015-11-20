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
        public int Count { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmailAddress { get; set; }
        public string CustomerMobile { get; set; }

        #region Helper Attributes

        //public Customer Customer { get; set; }
        public List<SalesOrderItem> OrderItems { get; private set; }

        #endregion

        public SalesOrder()
        {
            OrderItems = new List<SalesOrderItem>();
            OrderTime = DateTime.Now;
        }

        //public void AddOrderItem(SalesOrderItem entity)
        //{
        //    entity.Total = entity.Quantity * entity.UnitPrice - entity.Discount;
        //    entity.SalesOrderId = Id;
        //    entity.SalesOrder = this;

        //    Count++;
        //    Discount += entity.Discount;
        //    Total += entity.Total;

        //    OrderItems.Add(entity);
        //}

        //public void RemoveOrderItem(SalesOrderItem entity)
        //{
        //    OrderItems.Remove(entity);
        //    Count--;
        //    Discount -= entity.Discount;
        //    Total -= entity.Total;
        //}
    }
}
