using CarPartShop.Database.Common;

namespace CarPartShop.Database.Models
{
    public class OrderProduct : BaseEntity,IAuditable
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        public string SizeName { get; set; }
        public string ColorName { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public string? OrderId { get; set; }
        public Order? Order { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
