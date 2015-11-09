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
        private readonly IStockslyUow uow;

        public ProductsController(IStockslyUow db)
        {
            uow = db;
        }

        [HttpPost]
        [Route("p")]
        public IHttpActionResult PostProduct(Product product)
        {
            if (product != null)
            {
                uow.Products.Add(product);
                uow.Commit();

                return Created<Product>("p/{id}", product);
            }
            return BadRequest();
        }
    }
}
