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
        
        [HttpGet]
        [Route("suppliers")]
        public IHttpActionResult GetSuppliers()
        {
            IEnumerable<Supplier> suppliers = db.Suppliers.GetAll()
                .OrderBy(supplier => supplier.Name)
                .Take(100)
                .ToList();

            return Ok(new { Data = suppliers });
        }
        
        [HttpGet]
        [Route("categories")]
        public IHttpActionResult GetCategories()
        {
            IEnumerable<Category> categories = db.Categories.GetAll()
                .OrderBy(category => category.Name)
                .Take(100)
                .ToList();

            return Ok(new { Data = categories });
        }

        [HttpGet]
        [Route("customers")]
        public IHttpActionResult GetCustomers()
        {
            IEnumerable<Customer> customers = db.Customers.GetAll();
            return Ok(new { Data = customers });
        }

        [HttpGet]
        [Route("products")]
        public IHttpActionResult GetProducts()
        {
            IEnumerable<Product> products = db.Products.GetAll();
            return Ok(new { Data = products });
        }

        [HttpGet]
        [Route("pos")]
        public IHttpActionResult GetPurchaseOrders()
        {
            IEnumerable<PurchaseOrder> pos = db.PurchaseOrders.GetAll();
            return Ok(new { Data = pos });
        }

        [HttpGet]
        [Route("poitems")]
        public IHttpActionResult GetPurchaseOrderItems()
        {
            IEnumerable<PurchaseOrderItem> pos = db.PurchaseOrderItems.GetAll();
            return Ok(new { Data = pos });
        }

        [HttpGet]
        [Route("sos")]
        public IHttpActionResult GetSalesOrders()
        {
            IEnumerable<SalesOrder> pos = db.SalesOrders.GetAll();
            return Ok(new { Data = pos });
        }

        [HttpGet]
        [Route("soitems")]
        public IHttpActionResult GetSalesOrderItems()
        {
            IEnumerable<SalesOrderItem> pos = db.SalesOrderItems.GetAll();
            return Ok(new { Data = pos });
        }
    }
}
