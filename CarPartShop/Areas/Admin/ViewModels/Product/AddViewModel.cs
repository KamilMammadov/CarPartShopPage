namespace CarPartShop.Areas.Admin.ViewModels.Product
{
    public class AddViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public List<int> CategoryIds { get; set; }
        public List<int> SizeIds { get; set; }
        public List<int> ColorIds { get; set; }
        public List<int> TagIds { get; set; }
        public List<CategoryListViewModel>? Categories { get; set; }
        public List<SizeListViewModel>? Sizes { get; set; }
        public List<ColorListViewModel>? Colors { get; set; }
        public List<TagListViewModel>? Tags { get; set; }



    }

    public class CategoryListViewModel
    {
        public CategoryListViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class SizeListViewModel
    {
        public SizeListViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ColorListViewModel
    {
        public ColorListViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class TagListViewModel
    {
        public TagListViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
