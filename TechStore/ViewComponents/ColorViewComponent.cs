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
    public class ColorViewComponent : ViewComponent
    {
        private AppDbContext _context;
        public ColorViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Color> colors = await _context.Colors.Include(c=>c.ProductColors).ThenInclude(c=>c.Product).Where(c => c.IsDeleted == false).ToListAsync();
            if (colors == null && colors.Count() < 0)
            {
                return View("Not Found");
            }
            return View(await Task.FromResult(colors));
        }
    }
}
