namespace CarPartShop.Areas.Client.ViewModels
{
    public class BenefitListItemViewModel
    {
        public BenefitListItemViewModel(string tittle, string content, string imageUrl)
        {
            Tittle = tittle;
            Content = content;
            ImageUrl = imageUrl;
        }

        public string Tittle { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
    }
}
