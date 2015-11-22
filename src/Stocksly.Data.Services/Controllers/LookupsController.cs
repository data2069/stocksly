using Stocksly.Domain;
using Stocksly.Domain.Customers;
using Stocksly.Domain.Inventory;
using Stocksly.Domain.Purchasing;
using Stocksly.Domain.Sales;
using Stocksly.Domain.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Stocksly.Data.Services.Controllers
{
    [RoutePrefix("lookups")]
    public class LookupsController : ApiController
    {
        private readonly IStockslyUow db;

        public LookupsController() : this(new StockslyUow()) { }
        public LookupsController(IStockslyUow uow)
        {
            db = uow;
        }

        protected override void Dispose(bool disposing)
        {
            if (db != null && db is IDisposable)
            {
                ((IDisposable)db).Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        [Route("suppliers")]
        public IHttpActionResult GetSuppliers()
        {
            IEnumerable<Supplier> suppliers = db.Suppliers.GetAll()
                .OrderBy(supplier => supplier.Name)
                .Take(100)
                .ToList();

            return Ok(new { Result = suppliers });
        }
        
        [HttpGet]
        [Route("categories")]
        public IHttpActionResult GetCategories()
        {
            IEnumerable<Category> categories = db.Categories.GetAll()
                .OrderBy(category => category.Name)
                .Take(100)
                .ToList();

            return Ok(new { Result = categories });
        }

        [HttpGet]
        [Route("customers")]
        public IHttpActionResult GetCustomers()
        {
            IEnumerable<Customer> customers = db.Customers.GetAll().ToList();
            return Ok(new { Result = customers });
        }

        [HttpGet]
        [Route("products")]
        public IHttpActionResult GetProducts()
        {
            IEnumerable<Product> products = db.Products.GetAll().ToList();
            return Ok(new { Result = products });
        }

        [HttpGet]
        [Route("pos")]
        public IHttpActionResult GetPurchaseOrders()
        {
            IEnumerable<PurchaseOrder> pos = db.PurchaseOrders.GetAll().ToList();
            return Ok(new { Result = pos });
        }

        [HttpGet]
        [Route("poitems")]
        public IHttpActionResult GetPurchaseOrderItems()
        {
            IEnumerable<PurchaseOrderItem> pos = db.PurchaseOrderItems.GetAll().ToList();
            return Ok(new { Result = pos });
        }

        [HttpGet]
        [Route("sos")]
        public IHttpActionResult GetSalesOrders()
        {
            IEnumerable<SalesOrder> pos = db.SalesOrders.GetAll().ToList();
            return Ok(new { Result = pos });
        }

        [HttpGet]
        [Route("soitems")]
        public IHttpActionResult GetSalesOrderItems()
        {
            IEnumerable<SalesOrderItem> pos = db.SalesOrderItems.GetAll().ToList();
            return Ok(new { Result = pos });
        }
    }
}
