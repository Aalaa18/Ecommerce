using Ecommerce.DTO;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _category;

        public CategoryController(ICategoryService category)
        {
            _category = category;
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<CategoriesDTO>> Get()
        {
            var result = _category.GetAll();
            if (result.Count == 0)
            {
                return NotFound("No Categories");
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task  <IActionResult> Getbyid(int id) 
        {
            var result = await _category.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(string Namecategory)
        {
            var result =await _category.AddCategory(Namecategory);
            if (result == "Category Already Exists") return BadRequest(result);
            
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result= await _category.RemoveCategory(id);
            if(result == "Incorrect Id") return NotFound();
            return Ok(result);
        }
    }
}
