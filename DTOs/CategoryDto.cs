namespace ProductCategoryApi.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
        public object Category { get; internal set; }
    }
}
