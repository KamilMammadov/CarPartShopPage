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
    public class BlogController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public BlogController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> blogs = await _appDbContext.Blogs.Where(b => b.IsDeleted == false).ToListAsync();
            return View(blogs);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog, int page = 1)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (string.IsNullOrWhiteSpace(blog.Title))
            {
                ModelState.AddModelError("Title", "can not be empty");
                return View();
            }

            if (string.IsNullOrWhiteSpace(blog.Description))
            {
                ModelState.AddModelError("Description", "can not be empty");
                return View();
            }


            blog.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _appDbContext.Blogs.AddAsync(blog);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index", new { page });
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest("id can not be null");
            }
            Blog blog = await _appDbContext.Blogs.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);
            if (blog == null)
            {
                return NotFound("could not found");
            }
            return View(blog);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Blog blog, int? id)
        {
              Blog dbBlog = await _appDbContext.Blogs.FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);

            if (dbBlog == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(dbBlog);
            }
            if (blog.Id != id)
            {
                return NotFound("blog not found");
            }

            if (string.IsNullOrWhiteSpace(blog.Description))
            {
                ModelState.AddModelError("Description", "can not submit white space");
                return View(dbBlog);
            }
            if (string.IsNullOrWhiteSpace(blog.Title))
            {
                ModelState.AddModelError("Title", "can not submit white space");
                return View(dbBlog);
            }
            if (blog.File == null)
            {
                ModelState.AddModelError("File", "Sekil Mutleq Olmalidi");
                return View(dbBlog);
            }
            if (blog.File.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("File", "File type should be jpeg");
                return View();
            }
            if (blog.File.Length > 200000)
            {
                ModelState.AddModelError("File", "File length should be less than 20k");
                return View();
            }
            if (blog.File != null)
            {

                if (dbBlog.Image != null)
                {
                    Helper.DeleteFile(_env, dbBlog.Image, "assets", "images", "blog");
                }

                dbBlog.Image = blog.File.CreateFile(_env, "assets", "images", "blog");
            }

            dbBlog.Title = blog.Title.Trim().ToLower();
            dbBlog.Description = blog.Description.Trim().ToLower();
            dbBlog.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id, int page = 1)
        {
            if (id == null) return BadRequest("id can not be null");

            Blog dbBlog = await _appDbContext.Blogs
                .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);

            if (dbBlog == null) return NotFound();


            dbBlog.IsDeleted = true;
            dbBlog.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index", new { page });
        }
    }
}
