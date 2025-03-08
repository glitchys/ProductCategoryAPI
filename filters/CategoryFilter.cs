using ProductCategoryApi.Models;
using System;

namespace ProductCategoryApi.Filters
{
    public static class CategoryFilters
    {
        public static Predicate<Category> FilterByName(string name)
        {
            return category => string.IsNullOrEmpty(name) || category.Name.Contains(name, StringComparison.OrdinalIgnoreCase);
        }
         public static Predicate<Category> FilterByDescription(string description)
        {
            return category => string.IsNullOrEmpty(description) || category.Description.Contains(description, StringComparison.OrdinalIgnoreCase);
        }
        public static Predicate<Category> CombineFilters(Predicate<Category>[] filters)
        {
            return category =>
            {
                foreach (var filter in filters)
                {
                    if (!filter(category))
                    {
                        return false;
                    }
                }
                return true;
            };
        }
    }
}