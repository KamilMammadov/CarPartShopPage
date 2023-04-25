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
    public class CategoryController : Controller
    {
        public readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            IEnumerable<Category> categories = await _context.Categories
                .Include(c => c.Products)
                .Where(c=>c.IsDeleted==false)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
           
            ViewBag.PageIndex = page;
            ViewBag.PageCount = Math.Ceiling((double)categories.Count() / 5);
            return View(categories.Skip((page - 1) * 5).Take(5));
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.IsMain = await _context.Categories.Where(c => c.IsMain && !c.IsDeleted).ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category, int page = 1)
        {
            ViewBag.IsMain = await _context.Categories.Where(c => c.IsMain && !c.IsDeleted).ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (string.IsNullOrWhiteSpace(category.Name))
            {
                ModelState.AddModelError("Name", "can not be empty");
                return View();
            }


            if (await _context.Categories.AnyAsync(c => c.Name.ToLower().Trim() == category.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", "Already Exists");
                return View();
            }

            if (category.IsMain)
            {
                category.ParentId = null;

                if (category.CategoryImage == null)
                {
                    ModelState.AddModelError("CategoryImage", "image is required");
                    return View();
                }
                if (category.CategoryImage.ContentType != "image/png")
                {
                    ModelState.AddModelError("CategoryImage", "CategoryImage type should be png");
                    return View();
                }
                if (category.CategoryImage.Length> 20000)
                {
                    ModelState.AddModelError("CategoryImage", "CategoryImage length should be less than 20k");
                    return View();
                }
                category.Image = category.CategoryImage.CreateFile(_env, "assets", "images", "category-icon");
            }
            else
            {
                if (category.ParentId == null)
                {
                    ModelState.AddModelError("ParentId", "Parent Mutleq Secilmelidir");
                    return View();
                }

                if (!await _context.Categories.AnyAsync(c => c.Id == category.ParentId || c.IsDeleted || c.IsMain))
                {
                    ModelState.AddModelError("ParentId", "Parent Id yanlisdir");
                    return View();
                }
            }

            category.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { page });
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();

            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound();

            ViewBag.IsMain = await _context.Categories.Where(c => c.Id != id && c.IsMain && !c.IsDeleted).ToListAsync();


            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Category category, int page = 1)
        {
            ViewBag.IsMain = await _context.Categories.Where(c => c.Id != id && c.IsMain && !c.IsDeleted).ToListAsync();

            Category dbCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (dbCategory == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(dbCategory);
            }

            if (id != category.Id) return BadRequest();

            if (string.IsNullOrWhiteSpace(category.Name))
            {
                ModelState.AddModelError("Name", "Bosluq Olmamalidir");
                return View(dbCategory);
            }

            if (await _context.Categories.AnyAsync(t => t.Id != id && t.Name.ToLower() == category.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Already Exists");
                return View(dbCategory);
            }

            if (category.ParentId != null && id == category.ParentId)
            {
                ModelState.AddModelError("ParentId", "Please choose the correct parent");
                return View(category);
            }

            if (category.IsMain)
            {
                if (!dbCategory.IsMain && category.CategoryImage == null)
                {
                    ModelState.AddModelError("CategoryImage", "Sekil Mutleq Olmalidi");
                    return View(dbCategory);
                }
                if (category.CategoryImage.ContentType != "image/png")
                {
                    ModelState.AddModelError("CategoryImage", "CategoryImage type should be png");
                    return View();
                }
                if (category.CategoryImage.Length > 20000)
                {
                    ModelState.AddModelError("CategoryImage", "CategoryImage length should be less than 20k");
                    return View();
                }
                if (category.CategoryImage != null)
                {

                    if (dbCategory.Image != null)
                    {
                        Helper.DeleteFile(_env, dbCategory.Image, "assets", "images", "category-icon");
                    }

                    dbCategory.Image = category.CategoryImage.CreateFile(_env, "assets", "images", "category-icon");
                }

                dbCategory.ParentId = null;
            }
            else
            {
                if (category.ParentId == null)
                {
                    ModelState.AddModelError("ParentId", "Parent Mutleq Secilmelidir");
                    return View(dbCategory);
                }

                if (!await _context.Categories.AnyAsync(c => c.Id == category.ParentId && !c.IsDeleted && c.IsMain))
                {
                    ModelState.AddModelError("ParentId", "Parent Id yanlisdir");
                    return View(dbCategory);
                }

                dbCategory.ParentId = category.ParentId;
                Helper.DeleteFile(_env, dbCategory.Image, "assets", "images", "category-icon");
                dbCategory.Image = null;
            }

            dbCategory.IsMain = category.IsMain;
            dbCategory.Name = category.Name;
            dbCategory.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new {  page });
        }

        public async Task<IActionResult> Delete(int? id, int page = 1)
        {
            if (id == null) return BadRequest("id can not be null");

            Category dbCategory = await _context.Categories
                .Include(c => c.Products)
                .Include(c => c.Children)
                .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);

            if (dbCategory == null) return NotFound();

            if ((dbCategory.Products != null && dbCategory.Products.Count() > 0) || (dbCategory.Children != null && dbCategory.Children.Count() > 0))
            {
                TempData["Error"] = $"{id} li category siline bilmez";
                return RedirectToAction("Index");
                
            }

            dbCategory.IsDeleted = true;
            dbCategory.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new {  page });
        }
       
    }
}
