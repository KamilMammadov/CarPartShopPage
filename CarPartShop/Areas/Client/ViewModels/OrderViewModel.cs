using CarPartShop.Database.Models.Enums;

namespace CarPartShop.Areas.Client.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel(string id, DateTime date, OrderStatus status,
            int orderCount,decimal total)
        {
            Id = id;
            Date = date;
            Status = status;
            OrderCount = orderCount;
            Total = total;
        }

        public string Id { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
        public int OrderCount { get; set; }
        public decimal Total { get; set; }
    }
}
