namespace CarPartShop.Areas.Client.ViewModels
{
    public class ProductListViewModel
    {
        public ProductListViewModel(int id, string name, int price, string imageUrl)
        {
            Id = id;
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
