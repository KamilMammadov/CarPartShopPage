using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.DAL;
using TechStore.Models;

namespace TechStore.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class BrandController : Controller
    {
        public readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BrandController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            IEnumerable<Brand> brands = await _context.Brands
                .Include(c => c.Products)
                .Where(c => c.IsDeleted == false)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            ViewBag.PageIndex = page;
            ViewBag.PageCount = Math.Ceiling((double)brands.Count() / 5);
            return View(brands.Skip((page - 1) * 5).Take(5));
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand, int page = 1)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (string.IsNullOrWhiteSpace(brand.Name))
            {
                ModelState.AddModelError("Name", "can not be empty");
                return View();
            }


            if (await _context.Brands.AnyAsync(c => c.Name.ToLower().Trim() == brand.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", "Already Exists");
                return View();
            }

            brand.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { page });
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();

            Brand brand = await _context.Brands.FirstOrDefaultAsync(c => c.Id == id);

            if (brand == null) return NotFound();

            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Brand brand, int page = 1)
        {
            Brand dbBrand = await _context.Brands.FirstOrDefaultAsync(c => c.Id == id);

            if (dbBrand == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(dbBrand);
            }

            if (id != brand.Id) return BadRequest();

            if (string.IsNullOrWhiteSpace(brand.Name))
            {
                ModelState.AddModelError("Name", "Bosluq Olmamalidir");
                return View(dbBrand);
            }

            if (await _context.Brands.AnyAsync(t => t.Id != id && t.Name.ToLower() == brand.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Already Exists");
                return View(dbBrand);
            }

            dbBrand.Name = brand.Name;
            dbBrand.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { page });
        }

        public async Task<IActionResult> Delete(int? id, int page = 1)
        {
            if (id == null) return BadRequest("id can not be null");

            Brand dbBrand = await _context.Brands
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);

            if (dbBrand == null) return NotFound();

            if ((dbBrand.Products != null && dbBrand.Products.Count() > 0))
            {
                return RedirectToAction("Index");
            }

            dbBrand.IsDeleted = true;
            dbBrand.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { page });
        }

    }
}
