using CarPartShop.Areas.Client.ViewModels;
using CarPartShop.Contracts.File;
using CarPartShop.Database;
using CarPartShop.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace CarPartShop.Areas.Client.ViewComponents
{

    [ViewComponent(Name = "Product")]
    public class Product : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public Product(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = await _dataContext.Products.Select(p => new ProductListViewModel(
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
