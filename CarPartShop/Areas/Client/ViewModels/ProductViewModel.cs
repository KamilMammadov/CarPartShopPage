namespace CarPartShop.Areas.Client.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string MainImgUrl { get; set; }
        public List<CategoryViewModeL> Categories { get; set; }
        public List<ColorViewModeL> Colors { get; set; }
        public List<SizeViewModeL> Sizes { get; set; }
        public List<TagViewModel> Tags { get; set; }

        public ProductViewModel(int id, string name, string description, int price,
            string mainImgUrl, List<CategoryViewModeL> categories,
            List<ColorViewModeL> colors, List<SizeViewModeL> sizes, List<TagViewModel> tags)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            MainImgUrl = mainImgUrl;
            Categories = categories;
            Colors = colors;
            Sizes = sizes;
            Tags = tags;
        }


        public ProductViewModel() { }


        public class CategoryViewModeL
        {
            public CategoryViewModeL(string title, string parentTitle)
            {
                Title = title;
                ParentTitle = parentTitle;
            }

            public string Title { get; set; }
            public string ParentTitle { get; set; }


        }
        public class SizeViewModeL
        {
            public SizeViewModeL(string title)
            {
                Title = title;
            }

            public string Title { get; set; }
        }
        public class ColorViewModeL
        {
            public ColorViewModeL(string name)
            {
                Name = name;
            }

            public string Name { get; set; }
        }
        public class TagViewModel
        {
            public TagViewModel(string title)
            {
                Title = title;
            }

            public string Title { get; set; }
        }
    }
}
