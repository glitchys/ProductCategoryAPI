using Microsoft.AspNetCore.Mvc;
using ProductCategoryApi.Forms;
using ProductCategoryApi.Models;
using ProductCategoryApi.Services;
using ProductCategoryApi.Filters;
using ProductCategoryApi.DTOs;

namespace ProductCategoryApi.Controllers
{
    [ApiController]
    [Route("api/category")]
    [ServiceFilter(typeof(LoggingFilter))]
    public class CategoriesController(CategoryService categoryService) : ControllerBase
    {
        private readonly CategoryService _categoryService = categoryService;
        private Category category;

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter))]

        public IActionResult CreateCategory(Category CategoryDTO)
        {
            var createdCategory = _categoryService.CreateCategory(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = CategoryDTO.Id }, CategoryDTO);
        }
        [HttpGet]
        public IActionResult GetAllCategories([FromQuery] string name, [FromQuery] string description)
        {
            var categories = _categoryService.GetAllCategories(name, description);
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
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryForm form)
        {
            var updatedCategory = _categoryService.UpdateCategory(id, form);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            return Ok(updatedCategory);
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