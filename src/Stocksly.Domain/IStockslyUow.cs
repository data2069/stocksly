using Stocksly.Domain.Customers;
using Stocksly.Domain.Inventory;
using Stocksly.Domain.Purchasing;
using Stocksly.Domain.Sales;
using Stocksly.Domain.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocksly.Domain
{
    public interface IStockslyUow
    {
        IRepository<Customer> Customers { get; }
        IRepository<Category> Categories { get; }
        IRepository<Product> Products { get; }
        IRepository<PurchaseOrder> PurchaseOrders { get; }
        IRepository<PurchaseOrderItem> PurchaseOrderItems { get; }
        IRepository<SalesOrder> SalesOrders { get; }
        IRepository<SalesOrderItem> SalesOrderItems { get; }
        IRepository<Supplier> Suppliers { get; }

        void Commit();
    }
}
