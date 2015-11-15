using Stocksly.Domain;
using Stocksly.Domain.Inventory;
using Stocksly.Domain.Purchasing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Stocksly.Data.Services.Controllers
{
    public class InventoryController : ApiController
    {
        private readonly IStockslyUow db;

        public InventoryController(IStockslyUow uow)
        {
            db = uow;
        }

        [HttpGet]
        [Route("products/id/{id}")]
        public IHttpActionResult GetProducts(int id)
        {
            Product entity = db.Products.Find(id);
            if (entity != null)
            {
                return Ok(new { Data = entity });
            }
            return NotFound();
        }

        [HttpGet]
        [Route("products/search/name/{query}")]
        [Route("products/search/{query}/take/{count}")]
        [Route("products/name/{query}")]
        public IHttpActionResult GetProducts(string query, int count = 100)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                IEnumerable<Product> result = db.Products.GetAll()
                    .Where(product => product.Name.StartsWith(query))
                    .OrderBy(product => product.Name)
                    .Take(count)
                    .ToList();

                return Ok(new { Data = result });
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("products")]
        public IHttpActionResult PostProduct(Product product)
        {
            if (product != null)
            {
                db.Products.Add(product);
                db.Commit();

                return Created("p/id/{id}", new { id = product.Id });
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("products")]
        public IHttpActionResult Discontinue(dynamic model)
        {
            if (model != null)
            {
                Product entity = db.Products.Find(model.Id);
                db.Products.Delete(entity);
                db.Commit();

                return Ok(new { Data = entity });
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("products")]
        public IHttpActionResult Rebrand(dynamic model)
        {
            if (model != null)
            {
                Product original = db.Products.Find(model.Id);
                db.Products.Delete(original);

                Product rebranded = new Product
                {
                    Name = model.Name,
                    ReorderQuantity = original.ReorderQuantity,
                    CategoryId = original.CategoryId,
                    SupplierId = original.SupplierId
                };
                db.Products.Add(rebranded);

                db.Commit();

                return Ok(new { Data = rebranded });
            }
            return BadRequest(ModelState);
        }
    }
}
