using CarPartShop.Areas.Admin.ViewModels.ProductCategory;
using CarPartShop.Database;
using CarPartShop.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPartShop.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/Category")]
    public class CategoryController : Controller
    {
        private readonly DataContext _dataContext;


        public CategoryController(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        [HttpGet("list", Name = "admin-category-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Catagories.Select(s => new ListItemViewModel(s.Id, s.Title)).ToListAsync();

            return View(model);
        }

        [HttpGet("add", Name = "admin-category-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-category-add")]
        public async Task<IActionResult> Add(ListItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var category = new Category
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };


            await _dataContext.Catagories.AddAsync(category);
            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-category-list");
        }
        [HttpGet("update/{id}", Name = "admin-category-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var category = await _dataContext.Catagories.FirstOrDefaultAsync(n => n.Id == id);

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


        [HttpPost("update/{id}", Name = "admin-category-update")]
        public async Task<IActionResult> UpdateAsync(ListItemViewModel model)
        {
            var category = await _dataContext.Catagories.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (category is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            category.Title = model.Title;
          

            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-category-list");
        }



        [HttpPost("delete/{id}", Name = "admin-category-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var category = await _dataContext.Catagories.FirstOrDefaultAsync(n => n.Id == id);

            if (category is null)
            {
                return NotFound();
            }

            _dataContext.Catagories.Remove(category);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-category-list");
        }
    }
}
