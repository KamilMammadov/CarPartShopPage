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
    public class BrandViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public BrandViewComponent
            (AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Brand> brands = await _context.Brands.Where(c => c.IsDeleted == false).ToListAsync();
            if (brands == null && brands.Count() < 0)
            {
                return View("Not Found");
            }
            return View(await Task.FromResult(brands));
        }
    }
}
