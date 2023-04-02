using CarPartShop.Areas.Client.ViewModels;
using CarPartShop.Contracts.File;
using CarPartShop.Database;
using CarPartShop.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPartShop.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;


        public HomeController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }



        [HttpGet("~/", Name = "client-home-index")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

      
        [HttpGet("modal/{id}", Name = "plant-modal")]
        public async Task<ActionResult> ModalAsync(int id)
        {
            var plant = await _dataContext.Products.Include(p => p.ProductImages)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductSizes).FirstOrDefaultAsync(p => p.Id == id);


            if (plant is null)
            {
                return NotFound();
            }

            var model = new ModalViewModel(plant.Id,plant.Name, plant.Description, plant.Price,
                plant.ProductImages!.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(plant.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
            : String.Empty,
                _dataContext.ProductColors.Include(pc => pc.Color).Where(pc => pc.ProductId == plant.Id)
                .Select(pc => new ModalViewModel.ColorViewModeL(pc.Color.Name, pc.Color.Id)).ToList(),
                _dataContext.ProductSizes.Include(ps => ps.Size).Where(ps => ps.ProductId == plant.Id)
                .Select(ps => new ModalViewModel.SizeViewModeL(ps.Size.Name, ps.Size.Id)).ToList()
                );

            return PartialView("~/Areas/Client/Views/Shared/Partials/_ModalPartial.cshtml", model);
        }

    }
}
