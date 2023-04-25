using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.DAL;
using TechStore.Models;

namespace TechStore.ViewComponents
{
    public class CategorySliderViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public CategorySliderViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<CategorySlider> categorySliders = await _context.CategorySliders.Where(c => c.IsDeleted == false).ToListAsync();
            if (categorySliders == null && categorySliders.Count() < 0)
            {
                return View("Not Found");
            }
            return View(await Task.FromResult(categorySliders));
        }
    }
}
