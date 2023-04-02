namespace CarPartShop.Areas.Admin.ViewModels.PaymentBenefits
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string  Content { get; set; }
        public IFormFile? Image { get; set; }

        public string ImageUrl { get; set; }
    }
}
