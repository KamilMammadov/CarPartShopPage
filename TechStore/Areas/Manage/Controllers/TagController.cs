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
    public class TagController : Controller
    {
        public readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public TagController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            IEnumerable<Tag> tags = await _context.Tags
                .Include(c => c.Products)
                .Where(c => c.IsDeleted == false)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            ViewBag.PageIndex = page;
            ViewBag.PageCount = Math.Ceiling((double)tags.Count() / 5);
            return View(tags.Skip((page - 1) * 5).Take(5));
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag, int page = 1)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (string.IsNullOrWhiteSpace(tag.Name))
            {
                ModelState.AddModelError("Name", "can not be empty");
                return View();
            }


            if (await _context.Tags.AnyAsync(c => c.Name.ToLower().Trim() == tag.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", "Already Exists");
                return View();
            }
            
           

            tag.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { page });
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();

            Tag tag = await _context.Tags.FirstOrDefaultAsync(c => c.Id == id);

            if (tag == null) return NotFound();



            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Tag tag, int page = 1)
        {
           

            Tag dbTag = await _context.Tags.FirstOrDefaultAsync(c => c.Id == id);

            if (dbTag == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(dbTag);
            }

            if (id != tag.Id) return BadRequest();

            if (string.IsNullOrWhiteSpace(tag.Name))
            {
                ModelState.AddModelError("Name", "Bosluq Olmamalidir");
                return View(dbTag);
            }

            if (await _context.Tags.AnyAsync(t => t.Id != id && t.Name.ToLower() == tag.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Already Exists");
                return View(dbTag);
            }


            dbTag.Name = tag.Name;
            dbTag.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { page });
        }

        public async Task<IActionResult> Delete(int? id, int page = 1)
        {
            if (id == null) return BadRequest("id can not be null");

            Tag dbTag = await _context.Tags
                .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);

            if (dbTag == null) return NotFound();


            dbTag.IsDeleted = true;
            dbTag.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { page });
        }

    }
}
