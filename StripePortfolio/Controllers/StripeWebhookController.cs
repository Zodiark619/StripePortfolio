
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using StripePortfolio.Data;
using StripePortfolio.Models;

namespace StripePortfolio.Controllers
{

    [Route("api/stripe/webhook")]
    [ApiController]
    public class StripeWebhookController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;

        public StripeWebhookController(ApplicationDbContext dbContext, IConfiguration config)
        {
            _db = dbContext;
            _config = config;
        }
        [HttpPost]
        public async Task<IActionResult> Handle()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();
            var signature = Request.Headers["Stripe-Signature"];

            Event stripeEvent;

            try
            {
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    signature,
                    _config["Stripe:WebhookSecret"] 
                );
            }
            catch
            {
                return BadRequest();
            }

            switch (stripeEvent.Type)
            {
                case "checkout.session.completed":
                    var session = stripeEvent.Data.Object as Stripe.Checkout.Session;

                    var order = _db.Orders
                          .Include(o => o.Items).FirstOrDefault(o => o.StripeCheckoutSessionId == session.Id);

                    if (order == null)
                        break;

                    // Update order status
                    order.Status = "paid";
                    order.StripePaymentIntentId = session.PaymentIntentId;
                    order.UpdatedAt = DateTime.UtcNow;

                    // Add items to user inventory
                    foreach (var item in order.Items)
                    {
                        _db.UserInventories.Add(new UserInventory
                        {
                            UserId = order.UserId,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity
                        });
                    }

                    await _db.SaveChangesAsync();
                    break;
            }

            return Ok();
        }
    }

}

