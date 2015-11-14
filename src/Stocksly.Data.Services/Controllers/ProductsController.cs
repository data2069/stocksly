using Stocksly.Domain;
using Stocksly.Domain.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Stocksly.Data.Services.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IStockslyUow db;

        public ProductsController(IStockslyUow uow)
        {
            db = uow;
        }
        
        [HttpGet]
        [Route("p")]
        public IHttpActionResult GetProducts()
        {
            return Ok(new { Data = db.Products.GetAll().ToList() });
        }

        [HttpPost]
        [Route("p")]
        public IHttpActionResult PostProduct(Product product)
        {
            if (product != null)
            {
                db.Products.Add(product);
                db.Commit();

                return Created<Product>("p/{id}", product);
            }
            return BadRequest();
        }
    }
}
