using CarPartShop.Areas.Admin.ViewModels.ProductImage;
using CarPartShop.Contracts.File;
using CarPartShop.Database;
using CarPartShop.Database.Models;
using CarPartShop.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPartShop.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/ProductImage")]
    public class ProductImageController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public ProductImageController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        [HttpGet("{productId}/image/list", Name = "admin-productimage-list")]
        public async Task<IActionResult> List([FromRoute] int productId)
        {
            var product = await _dataContext.Products
                .Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == productId);
            if (product is null)
            {
                return NotFound();
            }
            var model = new ImagesViewModel { ProductId = product.Id };

            model.Images = product.ProductImages.Select(p=>new ImagesViewModel.ListItem
            {
                Id = p.Id,
                ImageUrl= _fileService.GetFileUrl(p.ImageNameInFileSystem, Contracts.File.UploadDirectory.Products),
                CreatedAt=p.CreatedAt
            }).ToList();

            return View(model);

        }

        [HttpGet("{productId}/image/add", Name = "admin-productimage-add")]
        public async Task<IActionResult> Add()
        {
            return View(new AddViewModel());
        }


        [HttpPost("{productId}/image/add", Name = "admin-productimage-add")]
        public async Task<IActionResult> Add([FromRoute]int productId,AddViewModel model)
        {
            if (!ModelState.IsValid)return View(model);

            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (product is null) return NotFound();

            var imageNameInSysten = await _fileService.UploadAsync(model.Image, Contracts.File.UploadDirectory.Products);

            var newImage = new ProductImage
            {
                Product = product,
                ImageName = model.Image.FileName,
                ImageNameInFileSystem = imageNameInSysten
            };

            await _dataContext.ProductImages.AddAsync(newImage);
            await _dataContext.SaveChangesAsync();

           return RedirectToRoute("admin-productimage-list",new { productId=productId});
        }

        [HttpPost("{productId}/image/{productImageId}/delete", Name = "admin-productimage-delete")]
        public async Task<IActionResult> Delete([FromRoute] int productId, [FromRoute] int productImageId)
        {
            var image= await _dataContext.ProductImages.FirstOrDefaultAsync(pi => pi.Id == productImageId);
            if(image is null) return NotFound();

            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product is null) return NotFound();

            await _fileService.DeleteAsync(image.ImageNameInFileSystem, UploadDirectory.Products);

            _dataContext.ProductImages.Remove(image);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-productimage-list", new { productId = productId });
        }
    }
}
