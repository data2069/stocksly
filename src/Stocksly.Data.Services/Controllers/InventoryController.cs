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

        public InventoryController() : this(new StockslyUow()) { }
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
                    .Where(product => product.DisplayName.StartsWith(query))
                    .OrderBy(product => product.DisplayName)
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

                return Created("products/id/" + product.Id, new { id = product.Id });
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("products/id/{id}/discontinue")]
        public IHttpActionResult Discontinue(int id)
        {
            if (id > 0)
            {
                Product entity = db.Products.Find(id);
                db.Products.Delete(entity);
                db.Commit();

                return Ok(new { Data = entity });
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("products/id/{id}/rebrand")]
        public IHttpActionResult Rebrand(int id, [FromBody]dynamic model)
        {
            if (model != null)
            {
                Product original = db.Products.Find(id);

                Product rebranded = new Product
                {
                    DisplayName = model.Name,
                    ReorderLevel = original.ReorderLevel,
                    CategoryId = original.CategoryId,
                    SupplierId = original.SupplierId
                };
                db.Products.Add(rebranded);
                db.Products.Delete(original);

                db.Commit();

                return Ok(new { Data = rebranded });
            }
            return BadRequest(ModelState);
        }
    }
}
