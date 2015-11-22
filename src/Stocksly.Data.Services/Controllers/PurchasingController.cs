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
    public class PurchasingController : ApiController
    {
        private readonly IStockslyUow db;

        public PurchasingController() : this(new StockslyUow()) { }
        public PurchasingController(IStockslyUow uow)
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

        [HttpPost]
        [Route("purchase")]
        public IHttpActionResult Purchase(PurchaseOrder order)
        {
            if (order != null)
            {
                foreach (PurchaseOrderItem orderItem in order.OrderItems)
                {
                    Product product = db.Products.GetAll()
                        .Where(prod => !prod.Discontinued)
                        .FirstOrDefault(prod => prod.Code == orderItem.ProductCode);

                    if (product == null)
                    {
                        ModelState.AddModelError(key: "ProductCode", errorMessage: orderItem.ProductCode + " not found or has been discontinued.");
                        continue;
                    }

                    product.Stocks += orderItem.Quantity;
                    db.Products.Update(product);

                    orderItem.ProductId = product.Id;
                    orderItem.ProductDisplayName = product.DisplayName;
                    orderItem.StocksAvailable = product.Stocks - orderItem.Quantity;
                }

                if (ModelState.IsValid)
                {
                    db.PurchaseOrders.Add(order);
                    db.Commit();

                    return Created(location: "purchaseorders/id/" + order.Id, content: new { id = order.Id });
                }
                return BadRequest(ModelState);
            }

            return Conflict();
        }
    }
}
