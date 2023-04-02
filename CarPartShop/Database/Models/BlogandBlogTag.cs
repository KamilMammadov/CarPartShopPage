using CarPartShop.Database.Common;

namespace CarPartShop.Database.Models
{
    public class BlogandBlogTag : BaseEntity
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public int BlogTagId { get; set; }
        public BlogTag BlogTag { get; set; }
    }
}
