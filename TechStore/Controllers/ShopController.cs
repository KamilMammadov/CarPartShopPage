using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.DAL;
using TechStore.Models;
using TechStore.ViewModels.SingleV;

namespace TechStore.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.PageIndex = page;
            IEnumerable<Product> products = await _context.Products.Include(p => p.Category).Where(s => s.IsDeleted == false).ToListAsync();
            if (products == null && products.Count() < 0)
            {
                return View("Not Found");
            }
            ViewBag.PageCount = Math.Ceiling((double)products.Count() / 5);

            return View(products.Skip((page - 1) * 5).Take(20));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            SingleVM singleVM = new SingleVM
            {
                RecentProduct = await _context.Products.Include(p => p.Category).Where(p => p.IsDeleted == false && p.IsNewArrival).ToListAsync(),
                Product = await _context.Products.Include(p => p.ProductImages).Include(p=>p.ProductColors).ThenInclude(p=>p.Color).Where(p => p.IsDeleted == false).ToListAsync(),
                ProductImages = await _context.ProductImages.Where(p => p.IsDeleted == false && p.ProductId == id).ToListAsync(),
                ProductSingle = await _context.Products.Include(p => p.Category).Include(ps=>ps.TechnicalSpec).FirstOrDefaultAsync(p => p.IsDeleted == false && p.Id == id),
                Descriptions = await _context.Descriptions.Where(D => D.IsDeleted == false && D.ProductId == id).ToListAsync()
            };

            if (singleVM == null)
            {
                return NotFound();
            }
            return View(singleVM);
        }
    }
}
