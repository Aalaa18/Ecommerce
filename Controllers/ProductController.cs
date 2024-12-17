using Ecommerce.DTO;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProducrservices _product;

        public ProductController(IProducrservices product)
        {
            _product=product;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> Get()
        {
            var result = _product.GetAll();
            if (result.Count == 0)
            {
                return NotFound("No Producrs");
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Getbyid(int id)
        {
            var result = await _product.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddCategory(ProductDto productDto)
        {
            var result = await _product.AddProduct(productDto);
            if (result == "Category Already Exists") return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _product.RemoveProduct(id);
            if (result == "Incorrect Id") return NotFound();
            return Ok(result);
        }


    }
}
