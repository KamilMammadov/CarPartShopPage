using CarPartShop.Database.Common;

namespace CarPartShop.Database.Models
{
    public class BlogImage : BaseEntity, IAuditable
    {
        public string ImageName { get; set; }
        public string ImageNameInFileSystem { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

