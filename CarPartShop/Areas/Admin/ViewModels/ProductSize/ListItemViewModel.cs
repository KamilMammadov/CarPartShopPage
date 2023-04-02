namespace CarPartShop.Areas.Admin.ViewModels.ProductSize
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ListItemViewModel()
        {
         
        }
    }
}
