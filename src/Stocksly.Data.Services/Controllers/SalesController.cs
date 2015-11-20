using Stocksly.Domain;
using Stocksly.Domain.Inventory;
using Stocksly.Domain.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Stocksly.Data.Services.Controllers
{
    public class SalesController : ApiController
    {
        private readonly IStockslyUow db;

        public SalesController() : this(new StockslyUow()) { }
        public SalesController(IStockslyUow uow)
        {
            db = uow;
        }

        [HttpPost]
        [Route("order")]
        public IHttpActionResult Order(SalesOrder order)
        {
            if (order != null)
            {
                foreach (SalesOrderItem orderItem in order.OrderItems)
                {
                    Product entity = db.Products.Find(orderItem.ProductId);
                    entity.Stocks -= orderItem.Quantity;

                    orderItem.ProductCode = entity.Code;
                    orderItem.ProductDisplayName = entity.DisplayName;
                }
                db.SalesOrders.Add(order);
                db.Commit();

                return Created("orders/id/" + order.Id, new { Id = order.Id });
            }

            return BadRequest(ModelState);
        }
    }
}
