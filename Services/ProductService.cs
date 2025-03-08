using ProductCategoryApi.DTOs;
using ProductCategoryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using ProductCategoryApi.Models;
using ProductCategoryApi.Forms;
using ProductCategoryApi.Filters;

namespace ProductCategoryApi.Services
{
    public class ProductService
    {
        private readonly CrudService<Product, ProductDto> _crudService;

        public ProductService()
        {
            _crudService = new CrudService<Product, ProductDto>();
        }

        public ProductDto CreateProduct(ProductForm form)
        {
            var Product = new Product
            {
                Name = form.Name,
                Price = form.Price,
            };

            var createdProduct = _crudService.Create(Product, (item, id) => item.Id = id, MapToDto);
            return createdProduct;
        }
        //filter product by (name,price,quantity)
        public List<ProductDto> GetAllProducts(string? name = null, decimal? minPrice = null,
        decimal? maxPrice = null, int? minQuantity = null, int? maxQuantity = null)
        {
            var filters = new Predicate<Product>[]
            {
                ProductFilters.FilterByName(name),
                ProductFilters.FilterByPrice(minPrice,maxPrice),
                ProductFilters.FilterByQuantity(minQuantity, maxQuantity)
            };
            var combinedFilter = ProductFilters.CombineFilters(filters);
            return _crudService.GetAll(MapToDto, combinedFilter); 
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