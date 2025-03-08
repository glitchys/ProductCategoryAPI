using ProductCategoryApi.DTOs;
using ProductCategoryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using ProductCategoryApi.Models;
using ProductCategoryApi.Forms;

namespace ProductCategoryApi.Services
{
    public class ProductService
    {
        private readonly CrudService<Product, ProductDto> _crudService;

        public ProductService()
        {
            _crudService = new CrudService<Product, ProductDto>();
        }

        public ProductDto CreateProduct(ProductForm product)
        {
            var Product = new Product
            {
                Name = product.Name,
                Price = product.Price,
            };

            return _crudService.Create(Product, (item, id) => item.Id = id, MapToDto);
        }
        //filter product by (name,price,quantity)
        public List<ProductDto> GetAllProducts(string? name = null, decimal? minPrice = null,
        decimal? maxPrice = null, int? minQuantity = null, int? maxQuantity = null)
        {
            var products = _crudService.GetAll(MapToDto);
            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value).ToList();
            }
            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price >= maxPrice.Value).ToList();
            }
            if (minQuantity.HasValue)
            {
                products = products.Where(p => p.Quantity >= minQuantity.Value).ToList();
            }
            if (maxQuantity.HasValue)
            {
                products = products.Where(p => p.Quantity >= maxQuantity.Value).ToList();
            }
            return products;
        }

        public ProductDto GetProductById(int id)
        {
            return _crudService.GetById(id, item => item.Id, MapToDto);
        }

        public ProductDto UpdateProduct(int id, Product updatedProduct)
        {
            return _crudService.Update(id, updatedProduct, item => item.Id, MapToDto);
        }

        public bool DeleteProduct(int id)
        {
            return _crudService.Delete(id, item => item.Id);
        }

        private ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }
    }
}