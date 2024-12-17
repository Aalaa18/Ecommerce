using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICartServices _cartServices;
        private readonly ApplicationDbcontext _context;

        public OrderController(ICartServices cartServices ,ApplicationDbcontext context)
        {
            _cartServices= cartServices;
            _context= context;
        }
        [HttpPost("CheckOut")]
        public async Task<IActionResult> makecheckout(string id)
        {
            var items = await _cartServices.GetyAllOrders(id);
            var options1 = new PaymentIntentCreateOptions
            {
                Amount = (long)await _cartServices.GetCartCost(id),
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" },
                Metadata = new Dictionary<string, string>
{
    { "OrderId", "12345" },  // Add any relevant metadata
    { "CustomerEmail", "aalaa@gmail.com" }
}
            };
            var service1 = new PaymentIntentService();
            PaymentIntent paymentIntent = await service1.CreateAsync(options1);
            var order = new Order
            {
                Amount = await _cartServices.GetCartCost(id),
                Date = DateTime.Now,
                Method = PaymentMethods.card,
                UserId = int.Parse(id)
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            int cartid = items.FirstOrDefault().cartid;
            foreach(var item in items)
            {
                OrderDetail orderdetails = new OrderDetail
                {
                    OrderId = order.Id,
                    Price = item.price,
                    ProductId = item.productid,
                    Quantity = item.quantity,
                };
                await _context.Orderdetails.AddAsync(orderdetails);
                await _context.SaveChangesAsync();
            }
            var cart= _context.carts.Where(x=>x.Id == cartid).FirstOrDefault();
            _context.carts.Remove(cart);
            var cartproduct=_context.cartProducts.Where(x=>x.CartId == cartid).ToList();
            _context.cartProducts.RemoveRange(cartproduct);
            _context.SaveChanges();

            return Ok(new { clientsecret = paymentIntent.ClientSecret });

        }

        [HttpGet("list-payments")]
        public async Task<IActionResult> ListPayments()
        {
            try
            {
                var service = new PaymentIntentService();
                var options = new PaymentIntentListOptions
                {
                    Limit = 10,  // Adjust this limit as necessary
                };

                var paymentIntents = await service.ListAsync(options);

                // Check if there are any payment intents
                if (paymentIntents.Data.Count == 0)
                {
                    return Ok(new { message = "No payments found." });
                }

                return Ok(paymentIntents);
            }
            catch (StripeException e)
            {
                return StatusCode(500, new { error = e.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}

/*
 * transfer cart to be order by storing cart data into order table and cartproducts into orderdetails
2- remove any cart and it`s products
3- send stripe order information
 */


//stripe code
/*

*/





/*
*/

//Stripe.net