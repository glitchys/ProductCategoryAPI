using ProductCategoryApi.Filters;
using Microsoft.AspNetCore.Mvc;
using ProductCategoryApi.Models;
using ProductCategoryApi.Services;
using System.Reflection.PortableExecutable;
namespace ProductCategoryApi.Forms

namespace ProductCategoryApi.Controllers
{
    // Controller for handling product-related HTTP requests
    [ApiController]
    [Route("api/products")]
    [ServiceFilter(typeof(LoggingFilter))]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductForm form)
        {
            var createdProduct = _productService.CreateProduct(form);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }
        [HttpGet]
        public IActionResult GetAllProducts([FromQuery] ProductFilterForm filter)
        {
            var products = _productService.GetAllProducts(name, minPrice, maxPrice, minQuantity, maxQuantity);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        //update 
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] CategoryForm form)
        {
            var product = _productService.UpdateProduct(id, updatedProductForm);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var result = _productService.DeleteProduct(id);
            if (result == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }

}