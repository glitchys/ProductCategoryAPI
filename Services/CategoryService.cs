using ProductCategoryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using ProductCategoryApi.Models;
using ProductCategoryApi.DTOs;
using ProductCategoryApi.Forms;
using ProductCategoryApi.Filters;
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
        public List<CategoryDTO> GetAllCategories(string name = null, string description = null)
        {
            var filters = new Predicate<Category>[]
            {
                CategoryFilters.FilterByName(name),
                CategoryFilters.FilterByDescription(description)
            };
            var combinedFilter = CategoryFilters.CombineFilters(filters);
            return _crudService.GetAll(MapToDto, combinedFilter); 
        }
        public CategoryDTO UpdateCategory(int id, CategoryForm form)
        {
            var existingCategory = GetCategoryById(id);
            if (existingCategory != null)
            {
                return null;
            }
            var updatedCategory = new Category
            {
                Id = id,
                Name = form.Name,
                Description = form.Description,
            };
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