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
    [Route("lookups")]
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
            return Ok(new { Data = db.Suppliers.GetAll().ToList() });
        }
        
        [HttpGet]
        [Route("categories")]
        public IHttpActionResult GetCategories()
        {
            return Ok(new { Data = db.Categories.GetAll().ToList() });
        }
    }
}
