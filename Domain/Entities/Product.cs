using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Product
    {

        public Guid ProductId { get; private  set; }
        public Name ProductName { get; private  set; } = default!;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public Guid ProductCategoryId { get; private set; }
        public virtual ProductCategory ProductCategory { get; private set; } = default!;

        public Product(Name productName, decimal price, string description, ProductCategory productCategory)
        {
            ProductName = productName;
            Description = description;
            Price = price;
            ProductCategory = productCategory;
        }
        private Product() { }

        public void Update(Name newName, string newDescription, decimal newPrice, ProductCategory newProductCategory)
        {
            ProductName = newName;
            Description = newDescription;
            Price = newPrice;
            UpdateProductCategory(newProductCategory);
        }

        public void UpdateProductCategory(ProductCategory newProductCategory)
        {
            if (newProductCategory == null)
                throw new ArgumentException("Product category cannot be empty.");
            ProductCategory = newProductCategory;
        }
    }
}
