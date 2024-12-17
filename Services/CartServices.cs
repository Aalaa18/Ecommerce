using Ecommerce.Data;
using Ecommerce.DTO;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class CartServices : ICartServices
    {
        private readonly ApplicationDbcontext _context;

        public CartServices(ApplicationDbcontext context)
        {

            _context = context;
        }


        public async Task<string> AddCart(CartProductDTo cartProductDTo)
        {
            var user = await _context.appUsers.FirstOrDefaultAsync(x => x.Id == cartProductDTo.user_id);
            if (user == null)
            {
                return "Incorrect  user_id ";
            }
            var cart = await _context.carts.FirstOrDefaultAsync(c => c.UserId == cartProductDTo.user_id);

            if (cart == null)
            {
                var newcart = new Cart
                {
                    UserId = cartProductDTo.user_id,
                };
                await _context.carts.AddAsync(newcart);
                await _context.SaveChangesAsync();
                user.CartId = newcart.Id;
                _context.appUsers.Update(user);
                await _context.SaveChangesAsync();
                cart = newcart;
            }
            var productFound = await _context.Products.AnyAsync(x => x.Id == cartProductDTo.productid);
            if (!productFound) { return "incorrect product_id"; }
            var existCart = await _context.cartProducts.FirstOrDefaultAsync(c => c.ProductId == cartProductDTo.productid && c.CartId == cart.Id);
            if (existCart != null)
            {

                existCart.Quantity++;
                _context.cartProducts.Update(existCart); // Explicitly mark the entity for update
                await _context.SaveChangesAsync();
            }
            else
            {
                var newcartproduct = new CartProduct
                {
                    CartId = cart.Id,
                    ProductId = cartProductDTo.productid,
                    Quantity = 1,
                };
                await _context.cartProducts.AddAsync(newcartproduct);
                await _context.SaveChangesAsync();
            }

            return "Product added to cart successfully.";










        }

        public async Task<long> GetCartCost(string userId)
        {
            long price = 0;
            var user = await _context.appUsers.SingleOrDefaultAsync(x=>x.Id==userId);
            if (user == null) return 0;
            var existCart = await _context.carts.SingleOrDefaultAsync(x => x.UserId == user.Id);

           
                var Cartproduct =  _context.cartProducts.Where(x => x.CartId == existCart.Id).ToList();

            foreach (var c in Cartproduct) {
                var productDetail = await _context.Products.FirstOrDefaultAsync(x => x.Id == c.ProductId);

                price += (long)productDetail.Price * c.Quantity;
            }
                return price;
            
        }

        public async Task<ICollection<OrderDTo>> GetyAllOrders(string id)
        {
            var cart= await _context.carts.SingleOrDefaultAsync(x=>x.UserId == id);
            if(cart==null)
            {
                cart = new Cart
                {
                    UserId = id,
                    CartProducts = new List<CartProduct>()
                };
            }
            return _context.cartProducts.Where(c=>c.CartId==cart.Id)
                .Select(cp=>new OrderDTo
                {
                    name=cp.Product.Name,
                    price=cp.Product.Price,
                    quantity=cp.Quantity,
                    cartid=cp.CartId,
                    productid=cp.ProductId
                    
                }).ToList();
                
               
        }
    }

}

/*
 post cart
{
  "productId": 2,
  "userId": "4"
}
----------------------
get cart param userid
return all cart products
-------------------
delete cart(delete cart product)
{
  "productId": 2,
  "userId": "1"
}
-------------------------
get cart cost
parm userid

return amount(money)
*/