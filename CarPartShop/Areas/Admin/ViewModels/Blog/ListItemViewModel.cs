namespace CarPartShop.Areas.Admin.ViewModels.Blog
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string title, string content, string from, DateTime createdAt,
            List<CategoryViewModel> categories, List<TagViewModel> tags)
        {
            Id = id;
            Title = title;
            Content = content;
            From = from;
            CreatedAt = createdAt;
            Categories = categories;
            Tags = tags;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string From { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<CategoryViewModel> Categories { get; set; }
   
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

   
    public class TagViewModel
    {
        public TagViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}

