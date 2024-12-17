using Ecommerce.DTO;

namespace Ecommerce.Services
{
    public interface ICartServices
    {
        Task<string> AddCart(CartProductDTo cartProductDTo);
        Task<long> GetCartCost(string userId);
        Task<ICollection<OrderDTo>> GetyAllOrders(string id);
    }
}