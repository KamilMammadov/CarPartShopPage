using CarPartShop.Database.Common;

namespace CarPartShop.Database.Models
{
    public class Category : BaseEntity, IAuditable
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
        public List<Category> Categories { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime UpdatedAt { get ; set ; }
    }
}
