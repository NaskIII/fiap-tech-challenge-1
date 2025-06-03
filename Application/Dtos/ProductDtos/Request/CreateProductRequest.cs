namespace Application.Dtos.ProductDtos.Request
{
    public class CreateProductRequest
    {
        public Guid? ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public Guid ProductCategoryId { get; set; }
    }
}
