namespace CarPartShop.Areas.Client.ViewModels
{
    public class ModalViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public List<ColorViewModeL> Colors { get; set; }
        public List<SizeViewModeL> Sizes { get; set; }
        public ModalViewModel(int id,string title, string description, decimal price, string ımgUrl, List<ColorViewModeL> colors, List<SizeViewModeL> sizes)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            ImgUrl = ımgUrl;
            Colors = colors;
            Sizes = sizes;
        }


        public class SizeViewModeL
        {
            public SizeViewModeL(string title, int id)
            {
                Title = title;
                Id = id;
            }

            public int Id { get; set; }
            public string Title { get; set; }
        }
        public class ColorViewModeL
        {
            public ColorViewModeL(string name, int id)
            {
                Name = name;
                Id = id;
            }
            public int Id { get; set; }
            public string Name { get; set; }
        }

    }
}
