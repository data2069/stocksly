using Stocksly.Domain;
using Stocksly.Domain.Customers;
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

        protected override void Dispose(bool disposing)
        {
            if (db != null && db is IDisposable)
            {
                ((IDisposable)db).Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        [Route("order")]
        public IHttpActionResult Order(SalesOrder order)
        {
            if (order != null)
            {
                Customer customer = db.Customers.Find(order.CustomerId);
                if (customer != null)
                {
                    order.Customer = customer;
                    order.CustomerName = customer.Name;
                    order.CustomerMobile = customer.Mobile;
                    order.CustomerEmailAddress = customer.EmailAddress;

                    foreach (SalesOrderItem orderItem in order.OrderItems)
                    {
                        Product product = db.Products.GetAll()
                            .Where(prod => !prod.Discontinued)
                            .FirstOrDefault(prod => prod.Code == orderItem.ProductCode);

                        if (product != null)
                        {
                            if (orderItem.Quantity < product.Stocks)
                            {
                                product.Stocks -= orderItem.Quantity;

                                orderItem.Product = product;
                                orderItem.ProductId = product.Id;
                                orderItem.UnitPrice = product.UnitPrice;
                                orderItem.ProductDisplayName = product.DisplayName;
                                orderItem.StocksRemaining = product.Stocks + orderItem.Quantity;

                                orderItem.CategoryId = product.CategoryId;
                            }
                            else
                            {
                                ModelState.AddModelError(key: "Quantity", errorMessage: orderItem.ProductCode + " insufficient stocks.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(key: "ProductCode", errorMessage: orderItem.ProductCode + " not found or has been discontinued.");
                        }
                    }

                    if (ModelState.IsValid)
                    {
                        db.SalesOrders.Add(order);
                        db.Commit();

                        return Created(location: "orders/id/" + order.Id, content: new { Id = order.Id });
                    }
                }
                else
                {
                    ModelState.AddModelError(key: "CustomerId", errorMessage: order.CustomerId + " not found.");
                }

                BadRequest(ModelState);
            }

            return Conflict();
        }
    }
}
