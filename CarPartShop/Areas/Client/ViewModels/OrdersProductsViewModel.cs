namespace CarPartShop.Areas.Client.ViewModels
{
    public class OrdersProductsViewModel
    {
        public List<ItemViewModel>? Products { get; set; }
        public SummaryViewModel? Summary { get; set; }


        public class SummaryViewModel
        {
            public decimal Total { get; set; }
        }

        public class ItemViewModel
        {
            public string? Name { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Total { get; set; }
            public string ColorName { get; set; }
            public int? ColorId { get; set; }

            public string SizeName { get; set; }
            public int? SizeId { get; set; }
        }
    }
}
