using CarPartShop.Database.Common;

namespace CarPartShop.Database.Models
{
    public class BlogCategory:BaseEntity,IAuditable
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public BlogCategory? Parent { get; set; }
        public List<BlogCategory> BlogCategories { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
