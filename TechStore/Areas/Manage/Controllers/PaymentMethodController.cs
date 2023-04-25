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
    public class PaymentMethodController : Controller
    {
        public readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public PaymentMethodController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<PaymentMethod> paymentMethods = await _context.PaymentMethod
                .Where(c => c.IsDeleted == false)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

           
            return View(paymentMethods);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();

            PaymentMethod paymentMethod = await _context.PaymentMethod.FirstOrDefaultAsync(c => c.Id == id);

            if (paymentMethod == null) return NotFound();



            return View(paymentMethod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, PaymentMethod paymentMethod)
        {

            PaymentMethod dbPayment = await _context.PaymentMethod.FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted==false);

            if (dbPayment == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(dbPayment);
            }

            if (id != paymentMethod.Id) return BadRequest();

            if (string.IsNullOrWhiteSpace(paymentMethod.Name))
            {
                ModelState.AddModelError("Name", "Bosluq Olmamalidir");
                return View(dbPayment);
            }

            if (await _context.PaymentMethod.AnyAsync(t => t.Id != id && t.Name.ToLower() == paymentMethod.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Already Exists");
                return View(dbPayment);
            }
            if (string.IsNullOrWhiteSpace(paymentMethod.Description))
            {
                ModelState.AddModelError("Description", "can not be empty");
                return View();
            }

                if (paymentMethod.PaymentImageFile == null)
                {
                    ModelState.AddModelError("PaymentImageFile", "Sekil Mutleq Olmalidi");
                    return View(dbPayment);
                }
                if (paymentMethod.PaymentImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("PaymentImageFile", "PaymentImageFile type should be png");
                    return View();
                }
                if (paymentMethod.PaymentImageFile.Length > 20000)
                {
                    ModelState.AddModelError("PaymentImageFile", "PaymentImageFile length should be less than 20k");
                    return View();
                }
                if (paymentMethod.PaymentImageFile != null)
                {

                    if (dbPayment.MainImage != null)
                    {
                        Helper.DeleteFile(_env, dbPayment.MainImage, "assets", "images", "payment");
                    }

                    dbPayment.MainImage = paymentMethod.PaymentImageFile.CreateFile(_env, "assets", "images", "payment");
                }


            dbPayment.Name = paymentMethod.Name;
            dbPayment.Description = paymentMethod.Description;
            dbPayment.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



    }
}
