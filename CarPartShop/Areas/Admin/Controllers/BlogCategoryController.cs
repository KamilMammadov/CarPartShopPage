using CarPartShop.Areas.Admin.ViewModels.ProductCategory;
using CarPartShop.Database;
using CarPartShop.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPartShop.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/BlogCategory")]
    public class BlogCategoryController : Controller
    {
        private readonly DataContext _dataContext;


        public BlogCategoryController(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        [HttpGet("list", Name = "admin-blogcategory-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.BlogCategories.Select(s => new ListItemViewModel(s.Id, s.Title)).ToListAsync();

            return View(model);
        }
        [HttpGet("add", Name = "admin-blogcategory-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-blogcategory-add")]
        public async Task<IActionResult> Add(ListItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var category = new BlogCategory
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };


            await _dataContext.BlogCategories.AddAsync(category);
            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-blogcategory-list");
        }

        [HttpGet("update/{id}", Name = "admin-blogcategory-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var category = await _dataContext.BlogCategories.FirstOrDefaultAsync(n => n.Id == id);

            if (category is null)
            {
                return NotFound();
            }

            var model = new ListItemViewModel
            {
                Id = category.Id,
                Title = category.Title,

            };

            return View(model);
        }


        [HttpPost("update/{id}", Name = "admin-blogcategory-update")]
        public async Task<IActionResult> UpdateAsync(ListItemViewModel model)
        {
            var category = await _dataContext.BlogCategories.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (category is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            category.Title = model.Title;
            category.UpdatedAt = DateTime.Now;


            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-blogcategory-list");
        }



        [HttpPost("delete/{id}", Name = "admin-blogcategory-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var category = await _dataContext.BlogCategories.FirstOrDefaultAsync(n => n.Id == id);

            if (category is null)
            {
                return NotFound();
            }

            _dataContext.BlogCategories.Remove(category);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blogcategory-list");
        }
    }
}
