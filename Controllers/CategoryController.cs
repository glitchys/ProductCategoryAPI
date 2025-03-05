using Microsoft.AspNetCore.Mvc;
using ProductCategoryApi.Forms;
using ProductCategoryApi.Models;
using ProductCategoryApi.Services;
using ProductCategoryApi.Filters;

namespace ProductCategoryApi.Controllers
{
    [ApiController]
    [Route("api/category")]
    [ServiceFilter(typeof(LoggingFilter))]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter))] 

        public IActionResult CreateCategory(CategoryForm form)
        {
            var category = new Category
            {
                Name = form.Name,
                Description = form.Description,
            };
            var CategoryDTO = _categoryService.CategoryDTO(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = CategoryDTO.Id }, CategoryDTO);
        }
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpDelete]
        public IActionResult DeleteCategoryById(int id)
        {
            var result = _categoryService.DeleteCategory(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}