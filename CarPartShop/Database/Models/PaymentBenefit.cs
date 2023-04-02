using CarPartShop.Database.Common;

namespace CarPartShop.Database.Models
{
    public class PaymentBenefit : BaseEntity, IAuditable
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string ImageInFileSystem { get; set; }
        public DateTime CreatedAt { get ; set; }
        public DateTime UpdatedAt { get ; set ; }
    }
}
