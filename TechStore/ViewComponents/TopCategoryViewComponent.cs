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
    public class TopCategoryViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public TopCategoryViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Category> categories = await _context.Categories.Where(s => s.IsDeleted == false).ToListAsync();
            if (categories == null && categories.Count() < 0)
            {
                return View("Not Found");
            }
            return View(await Task.FromResult(categories));
        }
    }
}
