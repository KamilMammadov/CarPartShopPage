using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.PageIndex = page;
            IEnumerable<Product> products = await _context.Products
                .Include(p => p.Category)
                .Include(p=>p.Brand)
                .Include(p=>p.ProductColors).ThenInclude(c => c.Color)
                .Where(p=>p.IsDeleted==false)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            ViewBag.PageCount = Math.Ceiling((double)products.Count() / 5);

            return View(products.Skip((page - 1) * 5).Take(5));
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.MainCategory = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Brand = await _context.Brands.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Color = await _context.Colors.Where(c => !c.IsDeleted).ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, int page = 1)
        {
            ViewBag.MainCategory = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Color = await _context.Colors.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Brand = await _context.Brands.Where(c => !c.IsDeleted).ToListAsync();


            if (!ModelState.IsValid) return View(product);
            if (product.ColorIds.Count == 0)
            {
                ModelState.AddModelError("", "color is required");
                return View(product);
            }
            if (!await _context.Categories.AnyAsync(c => c.Id == product.CategoryId && !c.IsDeleted))
            {
                ModelState.AddModelError("CategoryId", "Categoriya duzgun secilmiyib");
                return View(product);
            }
            if (!await _context.Brands.AnyAsync(c => c.Id == product.BrandId && !c.IsDeleted))
            {
                ModelState.AddModelError("BrandId", "Brand is not choosen correctly");
                return View(product);
            }
            if (product.ColorIds.Count != product.Counts.Count)
            {
                ModelState.AddModelError("", "Incorrect");
                return View();
            }
            if (product.ColorIds.Count() > 0)
            {
                List<ProductColor> productColors = new List<ProductColor>();
                for (int i = 0; i < product.ColorIds.Count; i++)
                {
                    ProductColor productColor = new ProductColor
                    {
                        ColorId = product.ColorIds[i],
                        Count = product.Counts[i]
                    };
                    productColors.Add(productColor);
                }
                product.ProductColors = productColors;
            }


            if (product.ProductFile== null)
            {
                ModelState.AddModelError("ProductFile", "image is required");
                return View(product);
            }
            if (product.ProductFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ProductFile", "ProductFile type should be jpeg");
                return View();
            }
            if (product.ProductFile.Length > 20000)
            {
                ModelState.AddModelError("ProductFile", "ProductFile length should be less than 20k");
                return View();
            }
            product.MainImage = product.ProductFile.CreateFile(_env, "assets", "images", "product");
           
            if (product.ProductImagesFile.Count() > 0)
            {
                if (product.ProductImagesFile.Count() > 10)
                {
                    ModelState.AddModelError("ProductImagesFile", "can not be more than 10 images");
                    return View(product);
                }
                List<ProductImage> productImages = new List<ProductImage>();
                foreach (IFormFile item in product.ProductImagesFile)
                {
                    if (item != null)
                    {
                        if (item.ContentType != "image/jpeg")
                        {
                            ModelState.AddModelError("ProductImagesFile", "ProductImagesFile type should be jpeg");
                            return View();
                        }
                        ProductImage productImage = new ProductImage
                        {
                            Image = item.CreateFile(_env, "assets", "images", "product"),
                            CreatedAt = DateTime.UtcNow.AddHours(4)
                        };
                        productImages.Add(productImage);
                    }

                }
                if (product.ProductImagesFile == null)
                {
                    ModelState.AddModelError("ProductImagesFile", "image is required");
                    return View(product);
                }
                if (product.ProductImagesFile.Length > 20000)
                {
                    ModelState.AddModelError("ProductImagesFile", "ProductFile length should be less than 20k");
                    return View();
                }
                product.ProductImages = productImages;
            }
            else
            {
                ModelState.AddModelError("ProductImagesFile", "images should be choosen");
                return View(product);
            }

            product.CreatedAt = DateTime.UtcNow.AddHours(4);
            product.Count = product.Counts.Sum();

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { page });
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.MainCategory = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Color = await _context.Colors.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Brand = await _context.Brands.Where(c => !c.IsDeleted).ToListAsync();

            if (id == null) return BadRequest();

            Product products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors).ThenInclude(c => c.Color)
                .Include(p=>p.Brand)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (products == null) return NotFound();

            products.ColorIds = products.ProductColors.Select(pt => pt.Color.Id).ToList();

            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Product product, int page = 1)
        {
            ViewBag.MainCategory = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Color = await _context.Colors.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Brand = await _context.Brands.Where(c => !c.IsDeleted).ToListAsync();

            Product dbproduct = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors).ThenInclude(c => c.Color)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (product.ColorIds.Count != product.Counts.Count)
            {
                ModelState.AddModelError("", "Incorrect Color or Count");
                return View(dbproduct);
            }

            if (!ModelState.IsValid)
            {
                return View(dbproduct);
            }

            if (id == null) return BadRequest();
            if (id != product.Id) return BadRequest();



            if (dbproduct == null) return NotFound();

            if (product.ProductFile == null)
            {
                ModelState.AddModelError("ProductFile", "image is required");
                return View(product);
            }
            if (product.ProductFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ProductFile", "ProductFile type should be jpeg");
                return View();
            }
            if (product.ProductFile.Length > 20000)
            {
                ModelState.AddModelError("ProductFile", "ProductFile length should be less than 20k");
                return View();
            }
            if (product.ProductImagesFile != null && product.ProductImagesFile.Length > (10 - dbproduct.ProductImages.Where(p => !p.IsDeleted).Count()))
            {
                ModelState.AddModelError("ProductImagesFile", "you exceed the limit");
                return View(dbproduct);
            }


            if (!await _context.Categories.AnyAsync(c => c.Id == product.CategoryId && !c.IsDeleted))
            {
                ModelState.AddModelError("CategoryId", "Category duzgun secilmeyib");
                return View(dbproduct);
            }

            if (!await _context.Brands.AnyAsync(c => c.Id == product.BrandId && !c.IsDeleted))
            {
                ModelState.AddModelError("BrandId", "Brand is not choosen correctly");
                return View(product);
            }
            foreach (int colorId in product.ColorIds)
            {
                if (!await _context.Colors.AnyAsync(s => s.Id == colorId))
                {
                    ModelState.AddModelError("", "Incorrect color");
                    return View();
                }
            }

            if (product.ColorIds.Count() > 0)
            {
                _context.ProductColors.RemoveRange(dbproduct.ProductColors);

                List<ProductColor> productColors = new List<ProductColor>();
                for (int i = 0; i < product.ColorIds.Count; i++)
                {
                    ProductColor productColor = new ProductColor
                    {
                        ColorId = product.ColorIds[i],
                        Count = product.Counts[i]
                    };
                    productColors.Add(productColor);
                }
                product.ProductColors = productColors;
                dbproduct.ProductColors = product.ProductColors;
            }
            else
            {
                ModelState.AddModelError("", "Renq mutlrq secmelisiniz");
                return View(dbproduct);
            }

            if (product.ProductFile != null)
            {

                Helper.DeleteFile(_env, dbproduct.MainImage, "assets", "images", "product");

                dbproduct.MainImage = product.ProductFile.CreateFile(_env, "assets", "images", "product");
            }

            if (product.ProductImagesFile != null && product.ProductImagesFile.Count() > 0)
            {
                List<ProductImage> productImages = new List<ProductImage>();
                foreach (IFormFile item in product.ProductImagesFile)
                {
                    if (item != null)
                    {
                        ProductImage productImage = new ProductImage
                        {
                            Image = item.CreateFile(_env, "assets", "images", "product"),
                            CreatedAt = DateTime.UtcNow.AddHours(4)
                        };
                        productImages.Add(productImage);
                    }

                }
                dbproduct.ProductImages.AddRange(productImages);
            }

            dbproduct.Count = product.Counts.Sum();
            dbproduct.CategoryId = product.CategoryId;
            dbproduct.Name = product.Name;
            dbproduct.Price = product.Price;
            dbproduct.DiscountedPrice = product.DiscountedPrice;
            dbproduct.BrandId = product.BrandId;
            dbproduct.infoText = product.infoText;
            dbproduct.Code = product.Code;
            dbproduct.Count = product.Count;
            dbproduct.Seria = product.Seria;
            dbproduct.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { page });
        }
        public async Task<IActionResult> Detail(int? id, int page = 1)
        {
            ViewBag.PageIndex = page;
            if (id == null) return BadRequest();

            Product products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors).ThenInclude(c => c.Color)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (products == null) return NotFound();

            return View(products);
        }

        public async Task<IActionResult> Delete(int? id, int page = 1)
        {
            if (id == null) return BadRequest();

            ViewBag.PageIndex = page;

            Product products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors).ThenInclude(c => c.Color)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (products == null) return NotFound();

            return View(products);
        }
        public async Task<IActionResult> DeleteProduct(int? id,  int page = 1)
        {
            if (id == null) return BadRequest();

            Product products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors).ThenInclude(c => c.Color)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (products == null) return NotFound();

            products.IsDeleted = true;
            products.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();

            return RedirectToAction("index", new { page });
        }

        public async Task<IActionResult> GetFormColorCount()
        {
            ViewBag.Color = await _context.Colors.Where(c => !c.IsDeleted).ToListAsync();

            return PartialView("_ProductColorPartial");
        }
      
    }
}
