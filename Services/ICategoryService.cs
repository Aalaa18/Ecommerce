using Ecommerce.DTO;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface ICategoryService
    {
        List<CategoriesDTO> GetAll();
        Task<CategoriesDTO> GetById(int id);
        Task<string> AddCategory(string name);
        Task<string> RemoveCategory(int id);
    
    }
}