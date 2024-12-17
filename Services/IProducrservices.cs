using Ecommerce.DTO;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IProducrservices
    {
        List<ProductDto> GetAll();
        Task<ProductDto> GetById(int id);
        Task<string> AddProduct(ProductDto productDto);
        Task<string> RemoveProduct(int id);
    }
}