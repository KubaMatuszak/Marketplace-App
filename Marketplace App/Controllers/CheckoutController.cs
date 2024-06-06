using Marketplace_App.Data;
using Marketplace_App.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace Marketplace_App.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly MarketplaceContext _marketplaceContext;
        public CheckoutController(MarketplaceContext marketplaceContext)
        {
            _marketplaceContext = marketplaceContext;
        }
        public IActionResult OrderConfirmation()
        {
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());
            if(session.PaymentStatus == "paid")
            {
                var product = _marketplaceContext.products.Find(TempData["Product"]);
                _marketplaceContext.products.Remove(product);
                _marketplaceContext.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("Index","Home"); 
        }
        public IActionResult Cart(Product product)
        {
            var domain = "https://localhost:7069/";
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"Checkout/OrderConfirmation",
                CancelUrl = domain + $"Checkout/OrderFailed",
                LineItems = new List<SessionLineItemOptions>(),
                Mode="payment",
            };
            var sessionListItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)product.Price*100,
                    Currency = "pln",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.Name,
                    }
                },
                Quantity = 1
            };
            options.LineItems.Add(sessionListItem);
            var service = new SessionService();
            Session session = service.Create(options);
            TempData["Session"] = session.Id;
            TempData["Product"] = product.ID;
            Response.Headers.Add("Location",session.Url);
            return new StatusCodeResult(303);
        }
    }
}
