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
            IEnumerable<Product> products = db.Products.GetAll()
                .OrderBy(p => p.Name)
                .Take(100)
                .ToList();

            return Ok(new { Data = products });
        }

        [HttpGet]
        [Route("p/id/{id}")]
        public IHttpActionResult GetProducts(int id)
        {
            Product entity = db.Products.Find(id);
            if (entity != null)
            {
                return Ok(new { Data = entity });
            }
            return NotFound();
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

        [HttpPost]
        [Route("p/id/{id}/purchase")]
        public IHttpActionResult Purchase(int id, dynamic model)
        {
            if (model != null)
            {
                Product entity = db.Products.Find(id);
                //entity.Quantity += model.Quantity;

                PurchaseOrder order = new PurchaseOrder { };
                db.PurchaseOrders.Add(order);

                db.Commit();

                return Ok(new { Data = order });
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("p/id/{id}/discontinue")]
        public IHttpActionResult Discontinue(int id, dynamic model)
        {
            if (model != null)
            {
                Product entity = db.Products.Find(id);
                db.Products.Delete(entity);
                db.Commit();

                return Ok(new { Data = entity });
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("p/id/{id}/rebrand")]
        public IHttpActionResult Rebrand(int id, dynamic model)
        {
            if (model != null)
            {
                Product original = db.Products.Find(id);
                db.Products.Delete(original);

                Product rebranded = new Product
                {
                    Name = model.Name,
                    ReorderLevel = original.ReorderLevel,
                    CategoryId = original.CategoryId,
                    SupplierId = original.SupplierId
                };
                db.Products.Add(rebranded);

                db.Commit();

                return Ok(new { Data = rebranded });
            }
            return BadRequest();
        }
    }
}
