using ProductCategoryApi.Models;
using ProductCategoryApi.DTOs;
using ProductCategoryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ProductCategoryApi.Services
{
    public class ProductService
    {
        private readonly CrudService<Product, ProductDto> _crudService;

        public ProductService()
        {
            _crudService = new CrudService<Product, ProductDto>();
        }

        public ProductDto CreateProduct(Product product)
        {
            return _crudService.Create(product, (item, id) => item.Id = id, MapToDto);
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