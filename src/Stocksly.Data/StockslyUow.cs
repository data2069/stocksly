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

        public IRepository<SalesOrder> SalesOrders
        {
            get
            {
                return GetRepository<SalesOrder>();
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
                { typeof(ICustomersRepository), new EFRepository<Customer>(db) },
                { typeof(ICategoriesRepository), new EFRepository<Category>(db) },
                { typeof(IProductsRepository), new EFRepository<Product>(db) },
                { typeof(IPurchaseOrdersRepository), new EFRepository<PurchaseOrder>(db) },
                { typeof(ISalesOrdersRepository), new EFRepository<SalesOrder>(db) },
                { typeof(ISuppliersRepository), new EFRepository<Supplier>(db) }
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
