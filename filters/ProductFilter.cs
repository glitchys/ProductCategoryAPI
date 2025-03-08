using ProductCategoryApi.Models;
using System;
using System.Reflection.Metadata.Ecma335;
namespace ProductCategoryApi.Filters
{
    public static class ProductFilters
    {
        public static Predicate<Product> FilterByName(string name)
        {
            return product => string.IsNullOrEmpty(name) || product.Name.Contains(name,
            StringComparison.OrdinalIgnoreCase);
        }
        //filter by price range 
        public static Predicate<Product> FilterByPrice(decimal? minPrice, decimal? maxPrice)
        {
            return product =>
            (!minPrice.HasValue || product.Price >= minPrice.Value) &&
            (!maxPrice.HasValue || product.Price <= maxPrice.Value);
        }
        //filter by quantity range 
        public static Predicate<Product> FilterByQuantity(int? minQuantity, int? maxQuantity)
        {
            return product =>
               (!minQuantity.HasValue || product.Quantity >= minQuantity.Value) &&
               (!maxQuantity.HasValue || product.Quantity <= maxQuantity.Value);
        }
        //combine all filters
        public static Predicate<Product> CombineFilters(Predicate<Product>[] filters)
        {
            return product =>
            {
                foreach (var filter in filters)
                {
                    if (!filter(product))
                    {
                        return false;
                    }
                }
                return true;
            };
        }
    }
}