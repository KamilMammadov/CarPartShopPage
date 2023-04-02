using CarPartShop.Areas.Admin.ViewModels.ProductSize;
using CarPartShop.Database;
using CarPartShop.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPartShop.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/Size")]
    public class SizeController : Controller
    {
        private readonly DataContext _dataContext;


        public SizeController(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        [HttpGet("list", Name = "admin-size-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Sizes.Select(s=>new ListItemViewModel(s.Id,s.Name)).ToListAsync();

            return View(model);
        }

        [HttpGet("add", Name = "admin-size-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-size-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var size = new Size
            {
                Name = model.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };


            await _dataContext.Sizes.AddAsync(size);
            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-size-list");
        }



        [HttpGet("update/{id}", Name = "admin-size-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var size = await _dataContext.Sizes.FirstOrDefaultAsync(n => n.Id == id);

            if (size is null)
            {
                return NotFound();
            }

            var model = new ListItemViewModel
            {
                Id=size.Id,
                Name = size.Name,
               
            };

            return View(model);
        }
        [HttpPost("update/{id}", Name = "admin-size-update")]
        public async Task<IActionResult> UpdateAsync(ListItemViewModel model)
        {
            var size = await _dataContext.Sizes.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (size is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            size.Name = model.Name;
          

            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-size-list");
        }



        [HttpPost("delete/{id}", Name = "admin-size-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var size = await _dataContext.Sizes.FirstOrDefaultAsync(n => n.Id == id);

            if (size is null)
            {
                return NotFound();
            }

            _dataContext.Sizes.Remove(size);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-size-list");
        }
    }
}
