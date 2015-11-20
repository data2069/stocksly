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

        [HttpPost]
        [Route("purchase")]
        public IHttpActionResult Purchase(PurchaseOrder order)
        {
            if (order != null)
            {
                foreach (PurchaseOrderItem orderItem in order.OrderItems)
                {
                    Product entity = db.Products.Find(orderItem.ProductId);
                    entity.Stocks += orderItem.Quantity;
                    db.Products.Update(entity);

                    orderItem.ProductCode = entity.Code;
                    orderItem.ProductDisplayName = entity.DisplayName;
                }
                db.PurchaseOrders.Add(order);
                db.Commit();

                return Created("purchaseorders/id/" + order.Id, new { id = order.Id });
            }

            return BadRequest(ModelState);
        }
    }
}
