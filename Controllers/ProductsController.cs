using Microsoft.AspNetCore.Mvc;
using ProductCategoryApi.Models;
using ProductCategoryApi.Services;

namespace ProductCategoryApi.Controllers
{
    // Controller for handling product-related HTTP requests
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase{
        private readonly ProductService _productService;
        public ProductsController(ProductService productService){
            _productService = productService;
        }
        [HttpPost]
        public IActionResult CreateProduct(Product product){
            var createdProduct = _productService.CreateProduct(product);
            return CreatedAtAction (nameof(GetProductById), new {id = createdProduct.Id}, createdProduct);

        }
        [HttpPost]
        public IActionResult GetAllProducts(){
            var products= _productService.GetAllProducts();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById (int id){
            var product = _productService.GetProductById(id);
            if (product==null){
                return NotFound(); 
            }
            return Ok(product);
        }
        //update 
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id , Product updatedProduct){
            var product = _productService.UpdateProduct(id,updatedProduct);
            if (product==null){
                return NotFound();
            }
            return Ok(product);
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int id){
            var result = _productService.DeleteProduct(id);
            if (result==null){
                return NotFound();
            }
            return NoContent();
        }

    }

}