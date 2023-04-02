using CarPartShop.Areas.Client.ViewComponents;
using CarPartShop.Areas.Client.ViewModels;
using CarPartShop.Contracts.File;
using CarPartShop.Database;
using CarPartShop.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CarPartShop.Areas.Client.ViewModels.ProductViewModel;

namespace CarPartShop.Areas.Client.Controllers
{
    [Area("client")]
    [Route("shop")]
    public class ShopController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;


        public ShopController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }


        [HttpGet("index", Name = "client-shop-index")]
        public async Task<IActionResult> Index([FromQuery]int? categoryId, [FromQuery] int? colorId)
        {
           
            var model = await _dataContext.Products.Include(p=>p.ProductColors).Include(p=>p.ProductCategories).Where(
                        p => categoryId == null || p.ProductCategories.Any(pc=>pc.CategoryId == categoryId))
                        .Where(p => colorId == null || p.ProductColors.Any(pc => pc.ColorId == colorId))
                .Select(p => new ProductViewModel(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.ProductImages.Take(1).FirstOrDefault() != null
                        ? _fileService.GetFileUrl(p.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                        : String.Empty,
                    p.ProductCategories!.Select(pc => pc.Category).Select(c => new CategoryViewModeL(c.Title, c.Parent.Title)).ToList(),
                    p.ProductColors!.Select(pc => pc.Color).Select(c => new ColorViewModeL(c.Name)).ToList(),
                    p.ProductSizes!.Select(ps => ps.Size).Select(s => new SizeViewModeL(s.Name)).ToList(),
                    p.ProductTags!.Select(ps => ps.Tag).Select(s => new TagViewModel(s.Name)).ToList()
                    )).ToListAsync();

            return View(model);
        }

    }
}
