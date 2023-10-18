namespace Clean.Core.Models
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ProductFeature? ProductFeature { get; set; }
    }
}
