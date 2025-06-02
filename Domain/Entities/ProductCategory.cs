namespace Domain.Entities
{
    public class ProductCategory
    {

        public Guid ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; } = [];

    }
}
