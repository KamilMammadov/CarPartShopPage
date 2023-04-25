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
    public class BannerController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public BannerController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Banner> banners = await _appDbContext.Banners.Where(b => b.IsDeleted == false).ToListAsync();
            return View(banners);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest("id can not be null");
            }
            Banner banner = await _appDbContext.Banners.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);
            if (banner == null)
            {
                return NotFound("could not found");
            }
            return View(banner);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Banner banner,int? id)
        {
            Banner dbBanner = await _appDbContext.Banners.FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted==false);

            if (dbBanner == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(dbBanner);
            }
            if (banner.Id != id)
            {
                return NotFound("banner not found");
            }

            if (string.IsNullOrWhiteSpace(banner.Link))
            {
                ModelState.AddModelError("Link", "can not submit white space");
                return View(dbBanner);
            }
            if (banner.File == null)
            {
                ModelState.AddModelError("File", "Sekil Mutleq Olmalidi");
                return View(dbBanner);
            }
            if (banner.File.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("File", "File type should be jpeg");
                return View();
            }
            if (banner.File.Length > 20000)
            {
                ModelState.AddModelError("File", "File length should be less than 20k");
                return View();
            }
            if (banner.File != null)
            {

                if (dbBanner.Image != null)
                {
                    Helper.DeleteFile(_env, dbBanner.Image, "assets", "images", "Banner");
                }

                dbBanner.Image = banner.File.CreateFile(_env, "assets", "images", "Banner");
            }

            dbBanner.Link = banner.Link.Trim().ToLower();
            dbBanner.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
