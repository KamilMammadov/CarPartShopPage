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
    public class TestimonialController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public TestimonialController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Testimonial> testimonials = await _appDbContext.Testimonials.Where(b => b.IsDeleted == false).ToListAsync();
            return View(testimonials);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest("id can not be null");
            }
            Testimonial testimonial = await _appDbContext.Testimonials.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);
            if (testimonial == null)
            {
                return NotFound("could not found");
            }
            return View(testimonial);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Testimonial testimonial, int? id)
        {
            Testimonial dbTestimonial = await _appDbContext.Testimonials.FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);

            if (dbTestimonial == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(dbTestimonial);
            }
            if (testimonial.Id != id)
            {
                return NotFound("testimonial not found");
            }

            if (string.IsNullOrWhiteSpace(testimonial.Description))
            {
                ModelState.AddModelError("Description", "can not submit white space");
                return View(dbTestimonial);
            }
            if (string.IsNullOrWhiteSpace(testimonial.Title))
            {
                ModelState.AddModelError("Title", "can not submit white space");
                return View(dbTestimonial);
            }
            if (testimonial.File == null)
            {
                ModelState.AddModelError("File", "Sekil Mutleq Olmalidi");
                return View(dbTestimonial);
            }
            if (testimonial.File.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("File", "File type should be jpeg");
                return View();
            }
            if (testimonial.File.Length > 200000)
            {
                ModelState.AddModelError("File", "File length should be less than 20k");
                return View();
            }
            if (testimonial.File != null)
            {

                if (dbTestimonial.Image != null)
                {
                    Helper.DeleteFile(_env, dbTestimonial.Image, "assets", "images", "testimonial");
                }

                dbTestimonial.Image = testimonial.File.CreateFile(_env, "assets", "images", "testimonial");
            }

            dbTestimonial.Title = testimonial.Title.Trim().ToLower();
            dbTestimonial.Description = testimonial.Description.Trim().ToLower();
            dbTestimonial.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
