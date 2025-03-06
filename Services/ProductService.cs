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

        public List<ProductDto> GetAllProducts()
        {
            return _crudService.GetAll(MapToDto);
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