namespace Application.Dtos.ProductCategory.Response
{
    public class ProductCategorySummaryResponse
    {

        public Guid ProductCategoryId { get; private set; }
        public string ProductCategoryName { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
    }
}
