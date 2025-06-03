using Domain.ValueObjects;
using System.Xml.Linq;

namespace Domain.Entities
{
    public class ProductCategory
    {

        public Guid ProductCategoryId { get; private set; }
        public Name ProductCategoryName { get; private set; }
        public string Description { get; private set; } = string.Empty;

        public virtual ICollection<Product> Products { get; private set; } = [];

        public ProductCategory()
        {
            ProductCategoryName = default!;
        }

        public ProductCategory(Name name, string description)
        {
            ProductCategoryName = name;
            ProductCategoryName = name;
            Description = description;
        }

        public void UpdateName(Name newName)
        {
            ProductCategoryName = newName;
        }

        public void UpdateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Descrição da categoria não pode ser vazia.");

            Description = description;
        }
    }
}
