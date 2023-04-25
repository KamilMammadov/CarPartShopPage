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
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _context.Sliders.Where(s=>s.IsDeleted==false).OrderByDescending(t => t.CreatedAt).ToListAsync();
            return View(sliders);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (string.IsNullOrWhiteSpace(slider.Name))
            {
                ModelState.AddModelError("Name", "can not be empty");
                return View();
            }


            if (await _context.Sliders.AnyAsync(c => c.Name.ToLower().Trim() == slider.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", "Already Exists");
                return View();
            }
            if (string.IsNullOrWhiteSpace(slider.Description))
            {
                ModelState.AddModelError("Description", "can not be empty");
                return View();
            }

            if (string.IsNullOrWhiteSpace(slider.SalePrice.ToString()))
            {
                ModelState.AddModelError("SalePrice", "can not be empty");
                return View();
            }



            if (string.IsNullOrWhiteSpace(slider.RegularPrice.ToString()))
            {
                ModelState.AddModelError("RegularPrice", "can not be empty");
                return View();
            }



            if (string.IsNullOrWhiteSpace(slider.Link.ToString()))
            {
                ModelState.AddModelError("Link", "can not be empty");
                return View();
            }


            if (slider.SliderFile == null)
            {
                ModelState.AddModelError("SliderFile", "image is required");
                return View();
            }
            if (slider.SliderFile.ContentType != "image/png")
            {
                ModelState.AddModelError("SliderFile", "SliderFile type should be png");
                return View();
            }
            if (slider.SliderFile.Length > 400000)
            {
                ModelState.AddModelError("SliderFile", "SliderFile length should be less than 400k");
                return View();
            }
            slider.Image = slider.SliderFile.CreateFile(_env, "assets", "images", "homeSlider");


            slider.CreatedAt = DateTime.UtcNow.AddHours(4);


            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
            if (slider == null) return BadRequest();
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View(slider);
            }

            if (id == null) return NotFound();

            Slider dbslider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (dbslider == null) return BadRequest();
            if (string.IsNullOrWhiteSpace(slider.Name))
            {
                ModelState.AddModelError("Name", "can not be empty");
                return View();
            }


            if (await _context.Sliders.AnyAsync(c => c.Name.ToLower().Trim() == slider.Name.ToLower().Trim()))
            {
                ModelState.AddModelError("Name", "Already Exists");
                return View();
            }
            if (string.IsNullOrWhiteSpace(slider.Description))
            {
                ModelState.AddModelError("Description", "can not be empty");
                return View();
            }


         
            if (string.IsNullOrWhiteSpace(slider.SalePrice.ToString()))
            {
                ModelState.AddModelError("SalePrice", "can not be empty");
                return View();
            }


           
            if (string.IsNullOrWhiteSpace(slider.RegularPrice.ToString()))
            {
                ModelState.AddModelError("RegularPrice", "can not be empty");
                return View();
            }


            
            if (string.IsNullOrWhiteSpace(slider.Link.ToString()))
            {
                ModelState.AddModelError("Link", "can not be empty");
                return View();
            }
            if (slider.SliderFile == null)
            {
                ModelState.AddModelError("SliderFile", "image is required");
                return View();
            }
            if (slider.SliderFile.ContentType != "image/png")
            {
                ModelState.AddModelError("SliderFile", "SliderFile type should be png");
                return View();
            }
            if (slider.SliderFile.Length > 400000)
            {
                ModelState.AddModelError("SliderFile", "SliderFile length should be less than 400k");
                return View();
            }
            slider.Image = slider.SliderFile.CreateFile(_env, "assets", "images", "homeSlider");
            if (slider.SliderFile != null)
            {
                Helper.DeleteFile(_env, dbslider.Image, "assets", "images", "homeSlider");
                dbslider.Image  = slider.SliderFile.CreateFile(_env, "assets", "images", "homeSlider");
            }

            dbslider.Title = slider.Title;
            dbslider.Name = slider.Name;
            dbslider.Link = slider.Link;
            dbslider.Description = slider.Description;
            dbslider.RegularPrice = slider.RegularPrice;
            dbslider.SalePrice = slider.SalePrice;

            dbslider.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");


        }

        public async Task<IActionResult> Delete(int? id, int page = 1)
        {
            if (id == null) return BadRequest("id can not be null");

            Slider dbSlider = await _context.Sliders
                .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);

            if (dbSlider == null) return NotFound();

            IEnumerable<Slider> sliders = await _context.Sliders.Where(c => c.IsDeleted == false).ToListAsync();
            if (sliders == null || sliders.Count()<3)
            {
                return RedirectToAction("Index");

            }

            dbSlider.IsDeleted = true;
            dbSlider.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { page });
        }
    }
}
