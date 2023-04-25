using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.DAL;
using TechStore.Extension;
using TechStore.Models;

namespace TechStore.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ShopBannerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ShopBannerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            ShopBanner shopBanner = await _context.ShopBanners.FirstOrDefaultAsync(s => s.IsDeleted == false);
            return View(shopBanner);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            ShopBanner shop = await _context.ShopBanners.FirstOrDefaultAsync(m => m.Id == id);
            if (shop == null) return BadRequest();
            return View(shop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, ShopBanner shopBanner)
        {
            if (!ModelState.IsValid)
            {
                return View(shopBanner);
            }

            if (id == null) return NotFound();

            ShopBanner dbshop = await _context.ShopBanners.FirstOrDefaultAsync(m => m.Id == id);

            if (dbshop == null) return BadRequest();
            if (string.IsNullOrWhiteSpace(shopBanner.Link))
            {
                ModelState.AddModelError("Link", "can not be empty");
                return View();
            }
            if (shopBanner.ShopBannerFile == null)
            {
                ModelState.AddModelError("ShopBannerFile", "image is required");
                return View();
            }
            if (shopBanner.ShopBannerFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ShopBannerFile", "ShopBannerFile type should be png");
                return View();
            }
            if (shopBanner.ShopBannerFile.Length > 400000)
            {
                ModelState.AddModelError("ShopBannerFile", "ShopBannerFile length should be less than 400k");
                return View();
            }
            shopBanner.Image = shopBanner.ShopBannerFile.CreateFile(_env, "assets", "images", "shopBanner");
            if (shopBanner.ShopBannerFile != null)
            {
                Helper.DeleteFile(_env, dbshop.Image, "assets", "images", "shopBanner");
                dbshop.Image = shopBanner.ShopBannerFile.CreateFile(_env, "assets", "images", "shopBanner");
            }

            dbshop.Link = shopBanner.Link;
            dbshop.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");


        }

       
    }
}
