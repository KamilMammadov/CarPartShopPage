using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.DAL;
using TechStore.Models;

namespace TechStore.ViewComponents
{
    public class OurProductViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public OurProductViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           IEnumerable<Product> products = await _context.Products.Include(p=>p.Category).Where(c => c.IsDeleted == false).ToListAsync();
            if (products == null && products.Count() < 0)
            {
                return View("Not Found");
            }
            return View(await Task.FromResult(products));
        }
    }
}
