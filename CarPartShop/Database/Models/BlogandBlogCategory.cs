using CarPartShop.Database.Common;

namespace CarPartShop.Database.Models
{
    public class BlogandBlogCategory : BaseEntity
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int BlogCategoryId { get; set; }
        public BlogCategory BlogCategory { get; set; }

    }
}
