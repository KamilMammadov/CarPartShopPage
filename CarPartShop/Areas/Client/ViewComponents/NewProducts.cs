using CarPartShop.Areas.Client.ViewModels;
using CarPartShop.Contracts.File;
using CarPartShop.Database;
using CarPartShop.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPartShop.Areas.Client.ViewComponents
{
    [Area("client")]
    [ViewComponent(Name = "NewProducts")]
    public class NewProducts : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;


        public NewProducts(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Products.OrderByDescending(p=>p.CreatedAt).Take(4).Select(p => new ProductListViewModel(
               p.Id,
               p.Name,
               p.Price,
               p.ProductImages.Take(1).FirstOrDefault() != null
                   ? _fileService.GetFileUrl(p.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                   : String.Empty
               )).ToListAsync();

            return View(model);
        }
    }
}
