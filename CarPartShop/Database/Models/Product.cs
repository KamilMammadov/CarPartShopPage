using CarPartShop.Database.Common;

namespace CarPartShop.Database.Models
{
    public class Product : BaseEntity, IAuditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int? Rate { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<ProductTag> ProductTags { get; set; }
        public List<BasketProduct>? BasketProducts { get; set; }
        public List<OrderProduct>? OrderProducts { get; set; }
        public DateTime CreatedAt { get ; set; }
        public DateTime UpdatedAt { get ; set; }
    }
}
