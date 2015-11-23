using Stocksly.Domain;
using Stocksly.Domain.Inventory;
using Stocksly.Domain.Purchasing;
using Stocksly.Domain.Suppliers;
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
                Supplier supplier = db.Suppliers.Find(order.SupplierId);
                if (supplier != null)
                {
                    order.Supplier = supplier;
                    order.SupplierName = supplier.Name;
                    order.SupplierEmailAddress = supplier.EmailAddress;
                    order.SupplierCreditToken = supplier.CreditToken;

                    foreach (PurchaseOrderItem orderItem in order.OrderItems)
                    {
                        Product product = db.Products.GetAll()
                            .Where(prod => !prod.Discontinued)
                            .FirstOrDefault(prod => prod.Code == orderItem.ProductCode);

                        if (product != null)
                        {
                            product.Stocks += orderItem.Quantity;

                            orderItem.Product = product;
                            orderItem.ProductId = product.Id;
                            orderItem.UnitPrice = product.UnitPrice;
                            orderItem.ProductDisplayName = product.DisplayName;
                            orderItem.StocksAvailable = product.Stocks - orderItem.Quantity;

                            orderItem.CategoryId = product.CategoryId;
                        }
                        else
                        {
                            ModelState.AddModelError(key: "ProductCode", errorMessage: orderItem.ProductCode + " not found or has been discontinued.");
                        }
                    }

                    if (ModelState.IsValid)
                    {
                        db.PurchaseOrders.Add(order);
                        db.Commit();

                        return Created(location: "purchaseorders/id/" + order.Id, content: new { id = order.Id });
                    }
                }
                else
                {
                    ModelState.AddModelError(key: "SupplierId", errorMessage: order.SupplierId + " not found.");
                }

                return BadRequest(ModelState);
            }

            return Conflict();
        }
    }
}
