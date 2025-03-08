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
            //var createdProduct = _productService.CreateProduct(product);
            var product = new Product
            {
                Name = form.Name,
                Price = form.Price,
            };
            var createdProduct = _productService.CreateProduct(product);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);

        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            //here for the products name to be case insesitive match 
            if (!string.IsNullOrEmpty(productName))
            {
                products = products.Where(p.Name.Contains(productName, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            //min price filter
            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value).ToList();
            }
            //max price 
            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value).ToList();
            }
            if (minQuantity.HasValue)
            {
                products = products.Where(p => p.Quantity >= minQuantity.Value).ToList();
            }
            if (maxQuantity.HasValue)
            {
                products = products.Where(p => p.Quantity >= maxQuantity.Value).ToList();
            }
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
        public IActionResult UpdateProduct(int id, ProductForm updatedProductForm)
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