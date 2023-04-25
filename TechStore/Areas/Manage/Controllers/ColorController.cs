using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechStore.DAL;
using TechStore.Models;

namespace TechStore.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ColorController : Controller
    {
        private readonly AppDbContext _context;
        public ColorController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            IEnumerable<Color> colors = await _context.Colors
                .Include(t => t.ProductColors)
                .Where(c=>c.IsDeleted==false)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
            ViewBag.PageIndex = page;
            ViewBag.PageCount = Math.Ceiling((double)colors.Count() / 5);

            return View(colors.Skip((page - 1) * 5).Take(5));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Color color)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (string.IsNullOrWhiteSpace(color.Name))
            {
                ModelState.AddModelError("Name", "Bosluq Olmamalidir");
                return View();
            }

            if (!Regex.IsMatch(color.Name, @"^[a-zA-Z /&-]+$"))
            {
                ModelState.AddModelError("Name", "Yalniz herif ola biler");
                return View();
            }

            if (await _context.Colors.AnyAsync(s => s.Name.ToLower() == color.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Already Exists");
                return View();
            }

            color.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _context.Colors.AddAsync(color);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            Color color = await _context.Colors.FirstOrDefaultAsync(s => s.Id == id);
            if (color == null) return NotFound();
            return View(color);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Color color, int page = 1)
        {
            if (!ModelState.IsValid)
            {
                return View(color);
            }

            if (id == null) return BadRequest();

            if (id != color.Id) return NotFound();

            Color dbcolor = await _context.Colors.FirstOrDefaultAsync(s => s.Id == id);

            if (dbcolor == null) return NotFound();

            if (string.IsNullOrWhiteSpace(color.Name))
            {
                ModelState.AddModelError("Name", "Bosluq Olmamalidir");
                return View();
            }

            if (!Regex.IsMatch(color.Name, @"^[a-zA-Z /&-]+$"))
            {
                ModelState.AddModelError("Name", "Yalniz herif ola biler");
                return View();
            }

            if (await _context.Colors.AnyAsync(s => s.Name.ToLower() == color.Name.ToLower() && s.Id != color.Id))
            {
                ModelState.AddModelError("Name", "Already Exists");
                return View();
            }

            dbcolor.Name = color.Name;
            dbcolor.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { page });
        }

        public async Task<IActionResult> Delete(int? id, int page = 1)
        {
            if (id == null) return BadRequest();

            Color dbcolor = await _context.Colors.Include(c=>c.ProductColors).ThenInclude(c=>c.Product).FirstOrDefaultAsync(t => t.IsDeleted ==false && t.Id == id);

            if (dbcolor == null) return NotFound();
            if ((dbcolor.ProductColors != null && dbcolor.ProductColors.Count() > 0))
            {
                return RedirectToAction("Index");
            }
            dbcolor.IsDeleted = true;
            dbcolor.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new {  page });

        }


    }
}
