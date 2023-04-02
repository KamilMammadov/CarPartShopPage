using CarPartShop.Areas.Admin.ViewModels.ProductCategory;
using CarPartShop.Database;
using CarPartShop.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPartShop.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/Tag")]
    public class TagController : Controller
    {
        private readonly DataContext _dataContext;


        public TagController(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        [HttpGet("list", Name = "admin-tag-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Tags.Select(s => new ListItemViewModel(s.Id, s.Name)).ToListAsync();

            return View(model);
        }


        [HttpGet("add", Name = "admin-tag-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost("add", Name = "admin-tag-add")]
        public async Task<IActionResult> Add(ListItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var tag = new Tag
            {
                Name = model.Title,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };


            await _dataContext.Tags.AddAsync(tag);
            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-tag-list");
        }

        [HttpGet("update/{id}", Name = "admin-tag-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var tag = await _dataContext.Tags.FirstOrDefaultAsync(n => n.Id == id);

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


        [HttpPost("update/{id}", Name = "admin-tag-update")]
        public async Task<IActionResult> UpdateAsync(ListItemViewModel model)
        {
            var tag = await _dataContext.Tags.FirstOrDefaultAsync(n => n.Id == model.Id);
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
            return RedirectToRoute("admin-tag-list");
        }



        [HttpPost("delete/{id}", Name = "admin-tag-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var tag = await _dataContext.Tags.FirstOrDefaultAsync(n => n.Id == id);

            if (tag is null)
            {
                return NotFound();
            }

            _dataContext.Tags.Remove(tag);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-tag-list");
        }
    }
}
