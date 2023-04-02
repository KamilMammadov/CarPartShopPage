using CarPartShop.Database.Models;
using CarPartShop.Database;
using Microsoft.AspNetCore.Mvc;
using CarPartShop.Areas.Admin.ViewModels.ProductCategory;
using Microsoft.EntityFrameworkCore;

namespace CarPartShop.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/BlogTag")]
    public class BlogTagController : Controller
    {
        private readonly DataContext _dataContext;


        public BlogTagController(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        [HttpGet("list", Name = "admin-blogtag-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.BlogTags.Select(s => new ListItemViewModel(s.Id, s.Name)).ToListAsync();

            return View(model);
        }


        [HttpGet("add", Name = "admin-blogtag-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost("add", Name = "admin-blogtag-add")]
        public async Task<IActionResult> Add(ListItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var tag = new BlogTag
            {
                Name = model.Title,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };


            await _dataContext.BlogTags.AddAsync(tag);
            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-blogtag-list");
        }

        [HttpGet("update/{id}", Name = "admin-blogtag-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var tag = await _dataContext.BlogTags.FirstOrDefaultAsync(n => n.Id == id);

            if (tag is null)
            {
                return NotFound();
            }

            var model = new ListItemViewModel
            {
                Id = tag.Id,
                Title = tag.Name,

            };

            return View(model);
        }


        [HttpPost("update/{id}", Name = "admin-blogtag-update")]
        public async Task<IActionResult> UpdateAsync(ListItemViewModel model)
        {
            var tag = await _dataContext.BlogTags.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (tag is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            tag.Name = model.Title;
            tag.UpdatedAt = DateTime.Now;

            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-blogtag-list");
        }



        [HttpPost("delete/{id}", Name = "admin-blogtag-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var tag = await _dataContext.BlogTags.FirstOrDefaultAsync(n => n.Id == id);

            if (tag is null)
            {
                return NotFound();
            }

            _dataContext.BlogTags.Remove(tag);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blogtag-list");
        }
    }
}
