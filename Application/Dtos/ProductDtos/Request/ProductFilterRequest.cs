namespace Application.Dtos.ProductDtos.Request
{
    public class ProductFilterRequest
    {
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public Guid? ProductCategoryId { get; set; }
    }
}
