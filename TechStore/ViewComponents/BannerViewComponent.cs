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
    public class BannerViewComponent : ViewComponent
    {
        private AppDbContext _context;
        public BannerViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Banner> banners = await _context.Banners.Where(s => s.IsDeleted == false).ToListAsync();
            if (banners == null && banners.Count() < 0)
            {
                return View("Not Found");
            }
            return View(await Task.FromResult(banners));
        }
    }
}
