using Stocksly.Domain.Customers;
using Stocksly.Domain.Inventory;
using Stocksly.Domain.Purchasing;
using Stocksly.Domain.Sales;
using Stocksly.Domain.Suppliers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocksly.Data
{
    internal class StockslyDb : DbContext
    {
        public StockslyDb() : base(nameOrConnectionString: "")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderItem> SalesOrderItems { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
