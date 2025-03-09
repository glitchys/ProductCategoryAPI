using System.ComponentModel.DataAnnotations;
namespace ProductCategoryApi.Forms
{
    public class CategoryFilterForm
    {
        public required string name { get; set; }
        public required string description { get; set; }
    }
}