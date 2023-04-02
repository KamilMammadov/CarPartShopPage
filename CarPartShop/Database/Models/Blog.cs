using CarPartShop.Database.Common;

namespace CarPartShop.Database.Models
{
    public class Blog : BaseEntity,IAuditable
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string From { get; set; }

        public List<BlogImage> BlogImages { get; set; }
        public List<BlogandBlogTag> blogandBlogTags { get; set; }
        public List<BlogandBlogCategory> BlogandBlogCategories { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
