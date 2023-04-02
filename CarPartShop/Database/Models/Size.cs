using CarPartShop.Database.Common;

namespace CarPartShop.Database.Models
{
    public class Size : BaseEntity,IAuditable
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public List<BasketProduct> BasketProducts { get; set; }


    }
}
