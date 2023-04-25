using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.DAL;
using TechStore.Extension;
using TechStore.Models;

namespace TechStore.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class CategorySliderController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public CategorySliderController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<CategorySlider> categorySliders = await _appDbContext.CategorySliders
                .Where(b => b.IsDeleted == false).ToListAsync();
            return View(categorySliders);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest("id can not be null");
            }
            CategorySlider category = await _appDbContext.CategorySliders
                .FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);
            if (category == null)
            {
                return NotFound("could not found");
            }
            return View(category);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategorySlider category, int? id)
        {
            CategorySlider dbSlider = await _appDbContext.CategorySliders
                .FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);

            if (dbSlider == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(dbSlider);
            }
            if (category.Id != id)
            {
                return NotFound("categoryslider not found");
            }

            if (string.IsNullOrWhiteSpace(category.Name))
            {
                ModelState.AddModelError("Name", "can not submit white space");
                return View(dbSlider);
            }
            if (string.IsNullOrWhiteSpace(category.Title))
            {
                ModelState.AddModelError("Title", "can not submit white space");
                return View(dbSlider);
            }
            if (category.CategorySliderImage == null)
            {
                ModelState.AddModelError("CategorySliderImage", "Sekil Mutleq Olmalidi");
                return View(dbSlider);
            }
            if (category.CategorySliderImage.ContentType != "image/png")
            {
                ModelState.AddModelError("File", "File type should be png");
                return View();
            }
            if (category.CategorySliderImage.Length > 20000)
            {
                ModelState.AddModelError("CategorySliderImage", "CategorySliderImage length should be less than 20k");
                return View();
            }
            if (category.CategorySliderImage != null)
            {

                if (dbSlider.Image != null)
                {
                    Helper.DeleteFile(_env, dbSlider.Image, "assets", "images");
                }

                dbSlider.Image = category.CategorySliderImage.CreateFile(_env, "assets", "images");
            }

            dbSlider.Name = category.Name.Trim().ToLower();
            dbSlider.Title = category.Title.Trim().ToLower();
            dbSlider.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
