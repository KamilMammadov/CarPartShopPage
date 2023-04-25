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
    public class AboutController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public AboutController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<About> abouts = await _appDbContext.Abouts.Where(b => b.IsDeleted == false).ToListAsync();
            return View(abouts);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest("id can not be null");
            }
            About about = await _appDbContext.Abouts.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);
            if (about == null)
            {
                return NotFound("could not found");
            }
            return View(about);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(About about, int? id)
        {
            About dbAbout = await _appDbContext.Abouts.FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);

            if (dbAbout == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(dbAbout);
            }
            if (about.Id != id)
            {
                return NotFound("about not found");
            }

            if (string.IsNullOrWhiteSpace(about.Description))
            {
                ModelState.AddModelError("Description", "can not submit white space");
                return View(dbAbout);
            }
            if (string.IsNullOrWhiteSpace(about.Title))
            {
                ModelState.AddModelError("Title", "can not submit white space");
                return View(dbAbout);
            }
            if (about.File == null)
            {
                ModelState.AddModelError("File", "Sekil Mutleq Olmalidi");
                return View(dbAbout);
            }
            if (about.File.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("File", "File type should be jpeg");
                return View();
            }
            if (about.File.Length > 200000)
            {
                ModelState.AddModelError("File", "File length should be less than 20k");
                return View();
            }
            if (about.File != null)
            {

                if (dbAbout.Image != null)
                {
                    Helper.DeleteFile(_env, dbAbout.Image, "assets", "images", "about");
                }

                dbAbout.Image = about.File.CreateFile(_env, "assets", "images", "about");
            }

            dbAbout.Title = about.Title.Trim().ToLower();
            dbAbout.Description = about.Description.Trim().ToLower();
            dbAbout.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
