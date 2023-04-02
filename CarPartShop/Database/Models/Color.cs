using CarPartShop.Database.Common;

namespace CarPartShop.Database.Models
{
    public class Color : BaseEntity, IAuditable
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        List<ProductColor> ProductColors { get; set; }
        public List<BasketProduct> BasketProducts { get; set; }

    }
}
