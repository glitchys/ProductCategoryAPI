using System.ComponentModel.DataAnnotations;
namespace ProductCategoryApi.Forms
{
    public class CategoryForm{
        [Required]
        [MaxLength(100)]
        public string Name { get; set;}
        [Required]
        [MaxLength(400)]
        public string Description { get; set;}
        }
}