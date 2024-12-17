using Ecommerce.DTO;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices _cart;

        public CartController(ICartServices cart)
        {
            _cart = cart;   
        }

        [HttpPost]
        public async Task<IActionResult> AddCart(CartProductDTo cartProductDTo)
        {
            var result= await _cart.AddCart(cartProductDTo);
            if (result == "incorrect product_id" || result == "Incorrect  user_id ") return NotFound();

            return Ok(result);
        }

        [HttpGet("GetCartPrice")]
        public async Task<IActionResult> GetPrice(string user_id)
        {
            var result = await _cart.GetCartCost(user_id);
            if (result ==null) return NotFound();

            return Ok(result);
            
        }
    }
}
