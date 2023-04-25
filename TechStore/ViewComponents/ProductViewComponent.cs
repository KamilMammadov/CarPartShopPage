using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.DAL;
using TechStore.ViewComponentModels.ProductV;

namespace TechStore.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public ProductViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ProductVM productVM = new ProductVM
            {
                NewArrivals = await _context.Products.Include(p=>p.Category).Where(c => c.IsDeleted == false && c.IsNewArrival).ToListAsync(),
                BestSellers = await _context.Products.Include(p => p.Category).Where(c => c.IsDeleted == false && c.IsBestSeller).ToListAsync(),
                Featured = await _context.Products.Include(p => p.Category).Where(c => c.IsDeleted == false && c.IsFeatured).ToListAsync()
            };
            if (productVM == null)
            {
                return View("Not Found");
            }
            return View(await Task.FromResult(productVM));
        }
    }
}
