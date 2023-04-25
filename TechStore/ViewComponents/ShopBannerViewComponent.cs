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
    public class ShopBannerViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public ShopBannerViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           ShopBanner ShopBanner = await _context.ShopBanners.FirstOrDefaultAsync(c => c.IsDeleted == false);
            if (ShopBanner == null)
            {
                return View("Not Found");
            }
            return View(await Task.FromResult(ShopBanner));
        }
    }
}
