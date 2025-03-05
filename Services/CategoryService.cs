using ProductCategoryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using ProductCategoryApi.Models;

namespace ProductCategoryApi.DTOs;
namespace ProductCategoryApi.Services
{
    public class CategoryService
    {
        private readonly CrudService<Category, CategoryDTO> _crudService;
        public CategoryService()
        {
            _crudService = new CrudService<Category, CategoryDTO>();
        }
        public Category CreateCategory(Category category)
        {
            return _crudService.Create(category, (item, id) => item.Id = id, MapToDto);
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

        internal object CategoryDTO(Category category)
        {
            throw new NotImplementedException();
        }
    }
}