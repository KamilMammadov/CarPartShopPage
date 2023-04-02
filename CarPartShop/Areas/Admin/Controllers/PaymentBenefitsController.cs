
using CarPartShop.Areas.Admin.ViewModels.Navbar;
using CarPartShop.Areas.Admin.ViewModels.PaymentBenefits;
using CarPartShop.Contracts.File;
using CarPartShop.Database;
using CarPartShop.Database.Models;
using CarPartShop.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AddViewModel = CarPartShop.Areas.Admin.ViewModels.PaymentBenefits.AddViewModel;
using ListItemViewModel = CarPartShop.Areas.Admin.ViewModels.PaymentBenefits.ListItemViewModel;
using UpdateViewModel = CarPartShop.Areas.Admin.ViewModels.PaymentBenefits.UpdateViewModel;

namespace CarPartShop.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/paymentbenefits")]
    public class PaymentBenefitsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;



        public PaymentBenefitsController(DataContext dataContext,IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;

        }

        [HttpGet("paymentbenefits", Name = "admin-paymentbenefits-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.PaymentBenefits.Select(p => new ListItemViewModel(p.Id, p.Title, p.Content, _fileService
                 .GetFileUrl(p.ImageInFileSystem, UploadDirectory.PaymentBenefits))).ToListAsync();

            return View(model);
        }
        [HttpGet("add", Name = "admin-paymentbenefits-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-paymentbenefits-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.PaymentBenefits);

            var benefit = new PaymentBenefit
            {
                Title = model.Title,
                Content = model.Content,
                Image = model.Image.FileName,
                ImageInFileSystem = imageNameInSystem,
                CreatedAt=DateTime.Now,
                UpdatedAt=DateTime.Now
            };

            await _dataContext.PaymentBenefits.AddAsync(benefit);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-paymentbenefits-list");
        }

        [HttpGet("update/{id}", Name = "admin-paymentbenefits-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var benefit = await _dataContext.PaymentBenefits.FirstOrDefaultAsync(b => b.Id == id);

            if (benefit is null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = benefit.Id,
                Title = benefit.Title,
                Content = benefit.Content,
                ImageUrl = _fileService.GetFileUrl(benefit.ImageInFileSystem, UploadDirectory.PaymentBenefits),
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-paymentbenefits-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var benefit = await _dataContext.PaymentBenefits.FirstOrDefaultAsync(b=>b.Id==model.Id);
            if (benefit is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _fileService.DeleteAsync(benefit.ImageInFileSystem, UploadDirectory.PaymentBenefits);
            var imageNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.PaymentBenefits);

            benefit.Title = model.Title;
            benefit.Content = model.Content;
            benefit.Image = model.Image.FileName;
            benefit.ImageInFileSystem = imageNameInSystem;
            


            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-paymentbenefits-list");
        }

    }
}
