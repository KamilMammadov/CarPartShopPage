namespace CarPartShop.Areas.Admin.ViewModels.Navbar
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id,string title, int order, string url)
        {
            Id = id;
            Title = title;
            Order = order;
            Url = url;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public string Url { get; set; }
        
    }
}
