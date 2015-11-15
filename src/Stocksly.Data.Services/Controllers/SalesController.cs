﻿using Stocksly.Domain;
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
                    entity.Quantity -= orderItem.Quantity;
                }
                db.SalesOrders.Add(order);
                db.Commit();

                return Ok(new { Data = order });
            }

            return BadRequest(ModelState);
        }
    }
}