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
    public class SpecialOfferController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SpecialOfferController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<SpecialOffer> specialOffers = await _context.SpecialOffers
                .Include(s=>s.Product)
                .Where(s => s.IsDeleted == false).OrderByDescending(t => t.CreatedAt).ToListAsync();
            return View(specialOffers);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Product = await _context.Products.Where(c => !c.IsDeleted).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialOffer specialOffer)
        {
            ViewBag.Product = await _context.Products.Where(c => !c.IsDeleted).ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (string.IsNullOrWhiteSpace(specialOffer.Description))
            {
                ModelState.AddModelError("Description", "can not be empty");
                return View();
            }
            if (string.IsNullOrWhiteSpace(specialOffer.Discount.ToString()))
            {
                ModelState.AddModelError("Discount", "can not be empty");
                return View();
            }


            if (specialOffer.SpecialOfferFile == null)
            {
                ModelState.AddModelError("SpecialOfferFile", "image is required");
                return View();
            }
            if (specialOffer.SpecialOfferFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("SpecialOfferFile", "SpecialOfferFile type should be jpeg");
                return View();
            }
            if (specialOffer.SpecialOfferFile.Length > 400000)
            {
                ModelState.AddModelError("SpecialOfferFile", "SpecialOfferFile length should be less than 400k");
                return View();
            }
            specialOffer.Image = specialOffer.SpecialOfferFile.CreateFile(_env, "assets", "images");


            specialOffer.CreatedAt = DateTime.UtcNow.AddHours(4);


            await _context.SpecialOffers.AddAsync(specialOffer);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Product = await _context.Products.Where(c => !c.IsDeleted).ToListAsync();
            if (id == null) return NotFound();
            SpecialOffer special = await _context.SpecialOffers.FirstOrDefaultAsync(so => so.Id == id);
            if (special == null) return BadRequest();
            return View(special);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, SpecialOffer specialOffer)
        {
            ViewBag.Product = await _context.Products.Where(c => !c.IsDeleted).ToListAsync();
            if (!ModelState.IsValid)
            {
                return View(specialOffer);
            }

            if (id == null) return NotFound();

            SpecialOffer dbSpecial = await _context.SpecialOffers.FirstOrDefaultAsync(so => so.Id == id);

            if (dbSpecial == null) return BadRequest();
            if (string.IsNullOrWhiteSpace(specialOffer.Description))
            {
                ModelState.AddModelError("Description", "can not be empty");
                return View();
            }
            if (specialOffer.SpecialOfferFile == null)
            {
                ModelState.AddModelError("SpecialOfferFile", "image is required");
                return View();
            }
            if (specialOffer.SpecialOfferFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("SpecialOfferFile", "SpecialOfferFile type should be jpeg");
                return View();
            }
            if (specialOffer.SpecialOfferFile.Length > 400000)
            {
                ModelState.AddModelError("SpecialOfferFile", "SpecialOfferFile length should be less than 400k");
                return View();
            }
            specialOffer.Image = specialOffer.SpecialOfferFile.CreateFile(_env, "assets", "images");
            if (specialOffer.SpecialOfferFile != null)
            {
                Helper.DeleteFile(_env, dbSpecial.Image, "assets", "images");
                dbSpecial.Image = specialOffer.SpecialOfferFile.CreateFile(_env, "assets", "images");
            }

            dbSpecial.Description = specialOffer.Description;

            dbSpecial.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");


        }

        public async Task<IActionResult> Delete(int? id, int page = 1)
        {
            if (id == null) return BadRequest("id can not be null");

            SpecialOffer dbspecial = await _context.SpecialOffers
                .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);

            if ( dbspecial == null) return NotFound();

            IEnumerable<SpecialOffer> specialOffers = await _context.SpecialOffers.Where(c => c.IsDeleted == false).ToListAsync();
            if (specialOffers == null || specialOffers.Count() < 3)
            {
                return RedirectToAction("Index");

            }

            dbspecial.IsDeleted = true;
            dbspecial.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { page });
        }
    }
}
