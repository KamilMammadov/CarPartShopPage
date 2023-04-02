using CarPartShop.Areas.Client.ViewComponents;
using CarPartShop.Areas.Client.ViewModels;
using CarPartShop.Database;
using CarPartShop.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CarPartShop.Areas.Client.Controllers
{
    [Area("client")]
    [Route("basket")]
    public class BasketController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IBasketService _basketService;

        public BasketController(DataContext dataContext, IBasketService basketService)
        {
            _dataContext = dataContext;
            _basketService = basketService;
        }

        //[HttpGet("add/{id}", Name = "client-basket-add")]
        //public async Task<IActionResult> AddProductAsync([FromRoute] int id)
        //{
        //    var product = await _dataContext.Products.Include(p=>p.ProductImages).FirstOrDefaultAsync(b => b.Id == id);
        //    if (product is null)
        //    {
        //        return NotFound();
        //    }

        //    var productsCookieViewModel = await _basketService.AddBasketProductAsync(product);
        //    if (productsCookieViewModel.Any())
        //    {
        //        return ViewComponent(nameof(ShopCart), productsCookieViewModel);
        //    }

        //    return ViewComponent(nameof(ShopCart));
        //}

        [HttpPost("add", Name = "client-basket-add")]
        public async Task<IActionResult> AddProductAsync( ProductCookieViewModel model)
        {
            var product = await _dataContext.Products.Include(p=>p.ProductSizes)
                .Include(p=>p.ProductColors)
                .Include(p => p.ProductImages).FirstOrDefaultAsync(b => b.Id == model.Id);
            if (product is null)
            {
                return NotFound();
            }
            model.SizeId = model.SizeId ?? product.ProductSizes.First().SizeId;
            model.ColorId = model.ColorId ?? product.ProductColors.First().ColorId;
            if (model.Quantity==0)
            {
                model.Quantity = 1;
            }
            var productsCookieViewModel = await _basketService.AddBasketProductAsync(product,model);
            if (productsCookieViewModel.Any())
            {
                return ViewComponent(nameof(ShopCart), productsCookieViewModel);
            }

            return ViewComponent(nameof(ShopCart));
        }

        [HttpGet("delete/{id}", Name = "client-basket-delete")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] int id)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(b => b.Id == id);
            if (product is null)
            {
                return NotFound();
            }

            var productCookieValue = HttpContext.Request.Cookies["products"];
            if (productCookieValue is null)
            {
                return NotFound();
            }

            var productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue);
            productsCookieViewModel!.RemoveAll(pcvm => pcvm.Id == id);

            HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productsCookieViewModel));

            return ViewComponent(nameof(ShopCart));
        }
    }
}
