using CarPartShop.Database.Common;
using CarPartShop.Database.Models.Enums;

namespace CarPartShop.Database.Models
{
    public class Order : IAuditable
    {
        public string Id { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public List<OrderProduct>? OrderProducts { get; set; }
    }
}
