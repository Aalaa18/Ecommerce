using Ecommerce.Data;
using Ecommerce.DTO;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class Producrservices : IProducrservices
    {
        private readonly ApplicationDbcontext _context;

        public Producrservices(ApplicationDbcontext context)
        {
            _context = context;
        }

        public List<ProductDto> GetAll()
        {
            var product = _context.Products.ToList();
            var productdto = product.Select(product => new ProductDto
            {
                categoryId = product.CategoryId,
                description = product.Description,
                id = product.Id,
                name = product.Name,
                price = product.Price,
            }
            ).ToList();


            return productdto;
        }

        public async Task<ProductDto> GetById(int id)
        {

            var product = await _context.Products.SingleOrDefaultAsync(c => c.Id == id);
            if (product == null)
            {
                return null;
            }
            var Dtoproduct = new ProductDto
            {
                price=product.Price,
                name=product.Name,
                id=product.Id,
                 description=product.Description,
                 categoryId=product.CategoryId,
                 
                 
            };


            return Dtoproduct;
        }

        public async Task<string> AddProduct(ProductDto productDto)
        {
            var product = await _context.Products.SingleOrDefaultAsync(name => name.Id==productDto.id);
            if (product != null) return "Category Already Exists";

            var newproduct = new Product
            {
             Id=productDto.id,
             CategoryId=productDto.categoryId,
             Description=productDto.description,
             Name=productDto.name,
             Price=productDto.price,

            };
            await _context.Products.AddAsync(newproduct);
            await _context.SaveChangesAsync();
            return "Category Added Successfully ";
        }

        public async Task<string> RemoveProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return "Incorrect Id";

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return "Category Removed";
        }
    }
}
