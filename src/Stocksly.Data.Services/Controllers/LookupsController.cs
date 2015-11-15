using Stocksly.Domain;
using Stocksly.Domain.Inventory;
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
        
        public LookupsController(IStockslyUow uow)
        {
            db = uow;
        }
        
        [HttpGet]
        [Route("suppliers")]
        public IHttpActionResult GetSuppliers()
        {
            IEnumerable<Supplier> suppliers = db.Suppliers.GetAll()
                .OrderBy(supplier => supplier)
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
    }
}
