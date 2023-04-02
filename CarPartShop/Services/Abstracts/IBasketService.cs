using CarPartShop.Database.Models;
using CarPartShop.Areas.Client.ViewModels;

namespace CarPartShop.Services.Abstracts
{
    public interface IBasketService
    {
        Task<List<ProductCookieViewModel>> AddBasketProductAsync(Product product, ProductCookieViewModel model);

    }
}
