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
    public class ContactController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public ContactController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Contact> contacts = await _appDbContext.Contacts.Where(b => b.IsDeleted == false).ToListAsync();
            return View(contacts);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest("id can not be null");
            }
            Contact contact = await _appDbContext.Contacts.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);
            if (contact == null)
            {
                return NotFound("could not found");
            }
            return View(contact);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Contact contact, int? id)
        {
            Contact dbContact = await _appDbContext.Contacts.FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);

            if (dbContact == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(dbContact);
            }
            if (contact.Id != id)
            {
                return NotFound("blog not found");
            }

            if (string.IsNullOrWhiteSpace(contact.Description))
            {
                ModelState.AddModelError("Description", "can not submit white space");
                return View(dbContact);
            }
            if (string.IsNullOrWhiteSpace(contact.Title))
            {
                ModelState.AddModelError("Title", "can not submit white space");
                return View(dbContact);
            }
            if (contact.File == null)
            {
                ModelState.AddModelError("File", "Sekil Mutleq Olmalidi");
                return View(dbContact);
            }
            if (contact.File.ContentType != "image/png")
            {
                ModelState.AddModelError("File", "File type should be png");
                return View();
            }
            if (contact.File.Length > 200000)
            {
                ModelState.AddModelError("File", "File length should be less than 20k");
                return View();
            }
            if (contact.File != null)
            {

                if (dbContact.Image != null)
                {
                    Helper.DeleteFile(_env, dbContact.Image, "assets", "images");
                }

                dbContact.Image = contact.File.CreateFile(_env, "assets", "images");
            }

            dbContact.Title = contact.Title.Trim().ToLower();
            dbContact.Description = contact.Description.Trim().ToLower();
            dbContact.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

  
    }
}
