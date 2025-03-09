using System.ComponentModel.DataAnnotations;
namespace ProductCategoryApi.Forms
{
    public class ProductFilterForm
    {
        public required string name { get; set; }
        public decimal? minPrice { get; set; }
        public decimal? maxPrice { get; set; }
        public int? minQuantity { get; set; }
        public int? maxQuantity { get; set; }


    }
}