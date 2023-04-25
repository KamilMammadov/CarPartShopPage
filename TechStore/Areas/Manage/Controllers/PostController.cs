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
    public class PostController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public PostController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Post> posts = await _appDbContext.Posts.Where(b => b.IsDeleted == false).ToListAsync();
            return View(posts);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest("id can not be null");
            }
            Post post = await _appDbContext.Posts.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);
            if (post == null)
            {
                return NotFound("could not found");
            }
            return View(post);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Post post, int? id)
        {
            Post dbPost = await _appDbContext.Posts.FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);

            if (dbPost == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(dbPost);
            }
            if (post.Id != id)
            {
                return NotFound("post not found");
            }

            if (string.IsNullOrWhiteSpace(post.Description))
            {
                ModelState.AddModelError("Description", "can not submit white space");
                return View(dbPost);
            }
            if (string.IsNullOrWhiteSpace(post.Title))
            {
                ModelState.AddModelError("Title", "can not submit white space");
                return View(dbPost);
            }
            if (post.File == null)
            {
                ModelState.AddModelError("File", "Sekil Mutleq Olmalidi");
                return View(dbPost);
            }
            if (post.File.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("File", "File type should be jpeg");
                return View();
            }
            if (post.File.Length > 200000)
            {
                ModelState.AddModelError("File", "File length should be less than 20k");
                return View();
            }
            if (post.File != null)
            {

                if (dbPost.Image != null)
                {
                    Helper.DeleteFile(_env, dbPost.Image, "assets", "images", "about");
                }

                dbPost.Image = post.File.CreateFile(_env, "assets", "images", "about");
            }

            dbPost.Title = post.Title.Trim().ToLower();
            dbPost.Description = post.Description.Trim().ToLower();
            dbPost.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
