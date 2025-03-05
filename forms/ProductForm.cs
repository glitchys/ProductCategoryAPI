using System.ComponentModel.DataAnnotations;
namespace ProductCategoryApi.Forms
{
    public class ProductForm
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price Must Be A Positive Number And More Than 0.1$")]
        public decimal Price { get; set; }
    }
}