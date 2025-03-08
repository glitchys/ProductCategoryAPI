using ProductCategoryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using ProductCategoryApi.Models;
using ProductCategoryApi.DTOs;
namespace ProductCategoryApi.Services
{
    public class CategoryService
    {
        private readonly CrudService<Category, CategoryDTO> _crudService;
        public CategoryService(CrudService<Category, CategoryDTO> crudService)
        {
            _crudService = crudService;
        }
        public Category CreateCategory(Category category)
        {
            var newCategory = new Category // Renamed to `newCategory` to avoid conflict
            {
                Name = category.Name,
                Description = category.Description
            };

            return _crudService.Create(newCategory, (item, id) => item.Id = id, MapToDto);
        }
        public List<CategoryDTO> GetAllCategories()
        {
            return _crudService.GetAll(MapToDto);
        }
        public CategoryDTO GetCategoryById(int id)
        {
            return _crudService.GetById(id, item => item.Id, MapToDto);
        }
        public CategoryDTO UpdateCategory(int id, Category updatedCategory)
        {
            return _crudService.Update(id, updatedCategory, item => item.Id, MapToDto);
        }
        public bool DeleteCategory(int id)
        {
            return _crudService.Delete(id, item => item.Id);
        }
        private CategoryDTO MapToDto(Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}