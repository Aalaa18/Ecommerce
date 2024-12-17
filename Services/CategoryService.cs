using Ecommerce.Data;
using Ecommerce.DTO;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbcontext _context;

        public CategoryService(ApplicationDbcontext Context)
        {
            _context = Context;
        }

        public List<CategoriesDTO> GetAll()
        {
            var categories=_context.categories.Include(c => c.Products).ToList();

            var Categorydto = categories.Select(category => new CategoriesDTO
            {
                id = category.Id,
                Name = category.Name,
                ProductCount = category.Products.Count(),
            }).ToList();
            return Categorydto;
        }

        public async Task< CategoriesDTO> GetById(int id)
        {

            var category= await _context.categories.Include(c=>c.Products).SingleOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return null;
            }
            var DtoCategory = new CategoriesDTO
            {
                id = category.Id,
                Name = category.Name,
                ProductCount = category.Products.Count(),
            };

         
            return DtoCategory;
        }

        public async Task<string> AddCategory(string categoriesDTO)
        {
            var category = await _context.categories.SingleOrDefaultAsync(name=>name.Name==categoriesDTO);
            if (category != null) return "Category Already Exists";

            var newcategory = new Category
            {
                Name = categoriesDTO

            };
            await _context.categories.AddAsync(newcategory);
            await _context.SaveChangesAsync();  
            return "Category Added Successfully ";
        }

        public async Task<string> RemoveCategory(int id)
        {
            var category= await _context.categories.FindAsync(id);
            if (category == null) return "Incorrect Id";

            var product = _context.Products.Where(p => p.Id == category.Id).ToList();
            if (product.Count > 0)
            {
               
                foreach (var item in product)
                {
                    _context.Products.Remove(item);
                }
                return "Warning Any Related product will be removed";
            }
            _context.categories.Remove(category);
           
            await _context.SaveChangesAsync();
            return "Category Removed";
        }
    }
}
