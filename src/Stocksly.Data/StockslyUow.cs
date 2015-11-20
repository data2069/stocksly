using Stocksly.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocksly.Domain.Customers;
using Stocksly.Domain.Inventory;
using Stocksly.Domain.Purchasing;
using Stocksly.Domain.Sales;
using Stocksly.Domain.Suppliers;
using System.Data.Entity;
using System.Transactions;

namespace Stocksly.Data
{
    public class StockslyUow : IStockslyUow
    {
        private readonly StockslyDb db;
        private readonly IDictionary<Type, dynamic> repositories;

        public StockslyUow()
        {
            db = CreateDb();
            repositories = CreateRepositories();
        }

        public IRepository<Category> Categories
        {
            get
            {
                return GetRepository<Category>();
            }
        }

        public IRepository<Customer> Customers
        {
            get
            {
                return GetRepository<Customer>();
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                return GetRepository<Product>();
            }
        }

        public IRepository<PurchaseOrder> PurchaseOrders
        {
            get
            {
                return GetRepository<PurchaseOrder>();
            }
        }

        public IRepository<PurchaseOrderItem> PurchaseOrderItems
        {
            get
            {
                return GetRepository<PurchaseOrderItem>();
            }
        }

        public IRepository<SalesOrder> SalesOrders
        {
            get
            {
                return GetRepository<SalesOrder>();
            }
        }

        public IRepository<SalesOrderItem> SalesOrderItems
        {
            get
            {
                return GetRepository<SalesOrderItem>();
            }
        }

        public IRepository<Supplier> Suppliers
        {
            get
            {
                return GetRepository<Supplier>();
            }
        }

        public void Commit()
        {
            db.SaveChanges();
            //using (TransactionScope txn = new TransactionScope())
            //{
            //    db.SaveChanges();
            //    txn.Complete();
            //}
        }

        #region Helper Methods

        private StockslyDb CreateDb()
        {
            return new StockslyDb();
        }

        private Dictionary<Type, dynamic> CreateRepositories()
        {
            return new Dictionary<Type, dynamic>
            {
                { typeof(Customer), new EFRepository<Customer>(db) },
                { typeof(Category), new EFRepository<Category>(db) },
                { typeof(Product), new EFRepository<Product>(db) },
                { typeof(PurchaseOrder), new EFRepository<PurchaseOrder>(db) },
                { typeof(PurchaseOrderItem), new EFRepository<PurchaseOrderItem>(db) },
                { typeof(SalesOrder), new EFRepository<SalesOrder>(db) },
                { typeof(SalesOrderItem), new EFRepository<SalesOrderItem>(db) },
                { typeof(Supplier), new EFRepository<Supplier>(db) }
            };
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            dynamic repo;
            repositories.TryGetValue(typeof(T), out repo);
            return repo;
        }

        #endregion
    }
}
