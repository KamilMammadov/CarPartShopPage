namespace CarPartShop.Areas.Admin.ViewModels.Product
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string name, string description, int price, int? rate, DateTime createdAt,
            List<CategoryViewModel> categories, List<SizeViewModel> sizes, List<ColorViewModel> colors,
            List<TagViewModel> tags)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Rate = rate;
            CreatedAt = createdAt;
            Categories = categories;
            Sizes = sizes;
            Colors = colors;
            Tags = tags;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int? Rate { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<CategoryViewModel> Categories { get; set; }
        public List<SizeViewModel> Sizes { get; set; }
        public List<ColorViewModel> Colors { get; set; }
        public List<TagViewModel> Tags { get; set; }



    }

    public class CategoryViewModel
    {
        public CategoryViewModel(string name, string parent)
        {
            Name = name;
            Parent = parent;
        }

        public string Name { get; set; }
        public string Parent { get; set; }
    }

    public class SizeViewModel
    {
        public SizeViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

    public class ColorViewModel
    {
        public ColorViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

    public class TagViewModel
    {
        public TagViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
