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
    public class PartnerController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public PartnerController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;

        }
        public async Task<IActionResult> Index(int page = 1)
        {
            IEnumerable<Partner> partners = await _appDbContext.Partners.Where(b => b.IsDeleted == false).ToListAsync();
            ViewBag.PageIndex = page;
            ViewBag.PageCount = Math.Ceiling((double)partners.Count() / 5);
            return View(partners.Skip((page - 1) * 5).Take(5));
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Partner partner, int page = 1)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (string.IsNullOrWhiteSpace(partner.Link))
            {
                ModelState.AddModelError("Link", "can not be empty");
                return View();
            }
            if (partner.File == null)
            {
                ModelState.AddModelError("File", "image is required");
                return View();
            }
            if (partner.File.ContentType != "image/png")
            {
                ModelState.AddModelError("File", "File type should be png");
                return View();
            }
            if (partner.File.Length > 20000)
            {
                ModelState.AddModelError("File", "File length should be less than 20k");
                return View();
            }
            partner.Image = partner.File.CreateFile(_env, "assets", "images", "brand");

            partner.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _appDbContext.Partners.AddAsync(partner);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index", new { page });
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest("id can not be null");
            }
            Partner partner = await _appDbContext.Partners.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);
            if (partner == null)
            {
                return NotFound("could not found");
            }
            return View(partner);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Partner partner, int? id, int page = 1)
        {
            Partner dbpartner = await _appDbContext.Partners.FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);

            if (dbpartner == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(dbpartner);
            }
            if (partner.Id != id)
            {
                return NotFound("partner not found");
            }

            if (string.IsNullOrWhiteSpace(partner.Link))
            {
                ModelState.AddModelError("Link", "can not submit white space");
                return View(dbpartner);
            }
            
            if (partner.File == null)
            {
                ModelState.AddModelError("File", "Sekil Mutleq Olmalidi");
                return View(dbpartner);
            }
            if (partner.File.ContentType != "image/png")
            {
                ModelState.AddModelError("File", "File type should be png");
                return View();
            }
            if (partner.File.Length > 20000)
            {
                ModelState.AddModelError("File", "File length should be less than 20k");
                return View();
            }
            if (partner.File != null)
            {

                if (dbpartner.Image != null)
                {
                    Helper.DeleteFile(_env, dbpartner.Image, "assets", "images", "brand");
                }

                dbpartner.Image = partner.File.CreateFile(_env, "assets", "images", "brand");
            }

            dbpartner.Link = partner.Link.Trim().ToLower();
            dbpartner.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index", new { page });
        }
        public async Task<IActionResult> Delete(int? id, int page = 1)
        {
            if (id == null) return BadRequest("id can not be null");

            Partner dbpartner = await _appDbContext.Partners
                .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);

            if (dbpartner == null) return NotFound();

            IEnumerable<Partner> partners = await _appDbContext.Partners.Where(t => t.IsDeleted == false).ToListAsync();

            if (partners.Count() > 6)
            {
                dbpartner.IsDeleted = true;
                dbpartner.DeletedAt = DateTime.UtcNow.AddHours(4);
                await _appDbContext.SaveChangesAsync();

            }

            return RedirectToAction("Index", new { page });
        }
    }
}
