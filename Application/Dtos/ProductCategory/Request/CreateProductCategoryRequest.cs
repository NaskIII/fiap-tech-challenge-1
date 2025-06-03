using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.ProductCategory.Request
{
    public class CreateProductCategoryRequest
    {

        public Guid? ProductCategoryId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Product category name cannot exceed 100 characters.")]
        public string ProductCategoryName { get; set; } = string.Empty;

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
