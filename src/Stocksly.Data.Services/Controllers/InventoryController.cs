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

        protected override void Dispose(bool disposing)
        {
            if (db != null && db is IDisposable)
            {
                ((IDisposable)db).Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        [Route("products/code/{code}")]
        public IHttpActionResult GetProducts(string code)
        {
            Product product = db.Products.GetAll()
                .Where(prod => !prod.Discontinued)
                .FirstOrDefault(prod => prod.Code == code);

            if (product != null)
            {
                return Ok(new { Result = product });
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

                return Ok(new { Result = result });
            }
            return Conflict();
        }

        [HttpPost]
        [Route("products")]
        public IHttpActionResult PostProduct(Product product)
        {
            if (product != null)
            {
                if (string.IsNullOrWhiteSpace(product.Code))
                {
                    ModelState.AddModelError(key: "ProductCode", errorMessage: "Product must have a code.");
                }

                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    db.Commit();

                    return Created("products/code/" + product.Code, new { code = product.Code });
                }

                return BadRequest(ModelState);
            }
            return Conflict();
        }

        [HttpPost]
        [Route("products/code/{code}/discontinue")]
        public IHttpActionResult Discontinue(string code)
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                Product product = db.Products.GetAll()
                    .Where(prod => !prod.Discontinued)
                    .FirstOrDefault(prod => prod.Code == code);

                if (product != null)
                {
                    db.Products.Delete(product);
                    db.Commit();
                    return Ok(new { Result = product });
                }

                ModelState.AddModelError(key: "ProductCode", errorMessage: code + " not found or has been discontinued.");
                return BadRequest(ModelState);
            }
            return Conflict();
        }

        [HttpPost]
        [Route("products/code/{code}/rebrand")]
        public IHttpActionResult Rebrand(string code, [FromBody]dynamic model)
        {
            if (model != null)
            {
                Product original = db.Products.GetAll()
                    .Where(prod => !prod.Discontinued)
                    .FirstOrDefault(prod => prod.Code == code);

                if (original != null)
                {
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

                    return Created(location: "products/code/" + code, content: new { Result = rebranded });
                }

                ModelState.AddModelError(key: "ProductCode", errorMessage: code + " not found or has been discontinued.");
                return BadRequest(ModelState);
            }
            return Conflict();
        }
    }
}
