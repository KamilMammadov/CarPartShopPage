using CarPartShop.Areas.Admin.ViewModels.Product;
using CarPartShop.Database;
using CarPartShop.Database.Models;
using CarPartShop.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CarPartShop.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/Product")]
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
     

        public ProductController(DataContext dataContext)
        {
            _dataContext = dataContext;
          
        }


        [HttpGet("list", Name = "admin-product-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Products.Select(p => new ListItemViewModel(p.Id, p.Name, p.Description, p.Price,
                p.Rate, p.CreatedAt,
                p.ProductCategories.Select(pc => pc.Category).Select(c => new CategoryViewModel(c.Title, c.Parent.Title)).ToList(),
                p.ProductSizes.Select(ps => ps.Size).Select(s => new SizeViewModel(s.Name)).ToList(),
                p.ProductColors.Select(pc => pc.Color).Select(c => new ColorViewModel(c.Name)).ToList(),
                p.ProductTags.Select(pt => pt.Tag).Select(t => new TagViewModel(t.Name)).ToList()
                )).ToListAsync();

            return View(model);
        }


        #region Add

        [HttpGet("add", Name = "admin-product-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                Categories = await _dataContext.Catagories.Select(c => new CategoryListViewModel(c.Id, c.Title)).ToListAsync(),
                Sizes = await _dataContext.Sizes.Select(s => new SizeListViewModel(s.Id, s.Name)).ToListAsync(),
                Colors = await _dataContext.Colors.Select(c => new ColorListViewModel(c.Id, c.Name)).ToListAsync(),
                Tags = await _dataContext.Tags.Select(t => new TagListViewModel(t.Id, t.Name)).ToListAsync()
            };
            return View(model);
        }

        [HttpPost("add", Name = "admin-product-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            foreach (var id in model.CategoryIds)
            {
                if (! await _dataContext.Catagories.AnyAsync(c=>c.Id==id))
                {
                    ModelState.AddModelError(String.Empty, "something went wrong");
                    return GetView(model);
                }
            }

            foreach (var id in model.SizeIds)
            {
                if (!await _dataContext.Sizes.AnyAsync(s => s.Id == id))
                {
                    ModelState.AddModelError(String.Empty, "something went wrong");
                    return GetView(model);
                }
            }


            foreach (var id in model.ColorIds)
            {
                if (!await _dataContext.Colors.AnyAsync(c => c.Id == id))
                {
                    ModelState.AddModelError(String.Empty, "something went wrong");
                    return GetView(model);
                }
            }

            foreach (var id in model.TagIds)
            {
                if (!await _dataContext.Tags.AnyAsync(t => t.Id == id))
                {
                    ModelState.AddModelError(String.Empty, "something went wrong");
                    return GetView(model);
                }
            }


            AddProduct();

            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-product-list");

            IActionResult GetView(AddViewModel model)
            {

                model.Categories = _dataContext.Catagories
                   .Select(c => new CategoryListViewModel(c.Id, c.Title))
                   .ToList();

                model.Sizes = _dataContext.Sizes
                 .Select(c => new SizeListViewModel(c.Id, c.Name))
                 .ToList();

                model.Colors = _dataContext.Colors
                 .Select(c => new ColorListViewModel(c.Id, c.Name))
                 .ToList();

                model.Tags = _dataContext.Tags
                 .Select(c => new TagListViewModel(c.Id, c.Name))
                 .ToList();


                return View(model);
            }

            async void AddProduct()
            {
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Rate = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await _dataContext.Products.AddAsync(product);

                foreach (var categoryId in model.CategoryIds)
                {
                    var productCatagory = new ProductCategory
                    {
                        CategoryId = categoryId,
                        Product = product,
                    };

                    await _dataContext.ProductCatagories.AddAsync(productCatagory);
                }

                foreach (var colorId in model.ColorIds)
                {
                    var productColor = new ProductColor
                    {
                        ColorId = colorId,
                        Product = product,
                    };

                    await _dataContext.ProductColors.AddAsync(productColor);
                }

                foreach (var sizeId in model.SizeIds)
                {
                    var productSize = new ProductSize
                    {
                        SizeId = sizeId,
                        Product = product,
                    };

                    await _dataContext.ProductSizes.AddAsync(productSize);
                }

                foreach (var tagId in model.TagIds)
                {
                    var productTag = new ProductTag
                    {
                        TagId = tagId,
                        Product = product,
                    };

                    await _dataContext.ProductTags.AddAsync(productTag);
                }


            }
           
        }
        #endregion


        #region Update

        [HttpGet("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var product = await _dataContext.Products.Include(p => p.ProductCategories).Include(p => p.ProductSizes)
                .Include(p => p.ProductColors).Include(p => p.ProductTags).FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
            {
                return NotFound();
            }

            var newModel = new AddViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Categories = await _dataContext.Catagories.Select(c => new CategoryListViewModel(c.Id, c.Title)).ToListAsync(),
                CategoryIds = product.ProductCategories.Select(pc => pc.CategoryId).ToList(),

                Sizes = await _dataContext.Sizes.Select(c => new SizeListViewModel(c.Id, c.Name)).ToListAsync(),
                SizeIds = product.ProductSizes.Select(ps => ps.SizeId).ToList(),

                Colors = await _dataContext.Colors.Select(c => new ColorListViewModel(c.Id, c.Name)).ToListAsync(),
                ColorIds = product.ProductColors.Select(ps => ps.ColorId).ToList(),

                Tags = await _dataContext.Tags.Select(c => new TagListViewModel(c.Id, c.Name)).ToListAsync(),
                TagIds = product.ProductTags.Select(ps => ps.TagId).ToList(),
            };
            return View(newModel);
        }


        [HttpPost("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync(AddViewModel model)
        {
            var product = await _dataContext.Products.Include(p => p.ProductCategories).Include(p => p.ProductSizes)
             .Include(p => p.ProductColors).Include(p => p.ProductTags).FirstOrDefaultAsync(p => p.Id == model.Id);
            if (product is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            foreach (var id in model.CategoryIds)
            {
                if (!await _dataContext.Catagories.AnyAsync(c => c.Id == id))
                {
                    ModelState.AddModelError(String.Empty, "something went wrong");
                    return GetView(model);
                }
            }

            foreach (var id in model.SizeIds)
            {
                if (!await _dataContext.Sizes.AnyAsync(s => s.Id == id))
                {
                    ModelState.AddModelError(String.Empty, "something went wrong");
                    return GetView(model);
                }
            }


            foreach (var id in model.ColorIds)
            {
                if (!await _dataContext.Colors.AnyAsync(c => c.Id == id))
                {
                    ModelState.AddModelError(String.Empty, "something went wrong");
                    return GetView(model);
                }
            }

            foreach (var id in model.TagIds)
            {
                if (!await _dataContext.Tags.AnyAsync(t => t.Id == id))
                {
                    ModelState.AddModelError(String.Empty, "something went wrong");
                    return GetView(model);
                }
            }
            UpdateProductAsync();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");

            async Task UpdateProductAsync()
            {
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.UpdatedAt = DateTime.Now;

                #region Catagory
                var categoriesInDb = product.ProductCategories.Select(bc => bc.CategoryId).ToList();
                var categoriesToRemove = categoriesInDb.Except(model.CategoryIds).ToList();
                var categoriesToAdd = model.CategoryIds.Except(categoriesInDb).ToList();

                product.ProductCategories.RemoveAll(bc => categoriesToRemove.Contains(bc.CategoryId));

                foreach (var categoryId in categoriesToAdd)
                {
                    var productCatagory = new ProductCategory
                    {
                        CategoryId = categoryId,
                        Product = product,
                    };

                    await _dataContext.ProductCatagories.AddAsync(productCatagory);
                }
                #endregion

                #region Color
                var colorInDb = product.ProductColors.Select(bc => bc.ColorId).ToList();
                var colorToRemove = colorInDb.Except(model.ColorIds).ToList();
                var colorToAdd = model.ColorIds.Except(colorInDb).ToList();

                product.ProductColors.RemoveAll(bc => colorToRemove.Contains(bc.ColorId));


                foreach (var colorId in colorToAdd)
                {
                    var productColor = new ProductColor
                    {
                        ColorId = colorId,
                        Product = product,
                    };

                    await _dataContext.ProductColors.AddAsync(productColor);
                }
                #endregion


                #region Size
                var sizeInDb = product.ProductSizes.Select(bc => bc.SizeId).ToList();
                var sizeToRemove = sizeInDb.Except(model.SizeIds).ToList();
                var sizeToAdd = model.SizeIds.Except(sizeInDb).ToList();

                product.ProductSizes.RemoveAll(bc => sizeToRemove.Contains(bc.SizeId));


                foreach (var sizeId in sizeToAdd)
                {
                    var productSize = new ProductSize
                    {
                        SizeId = sizeId,
                        Product = product,
                    };

                    await _dataContext.ProductSizes.AddAsync(productSize);
                }

                #endregion

                #region Tag
                var tagInDb = product.ProductTags.Select(bc => bc.TagId).ToList();
                var tagToRemove = tagInDb.Except(model.TagIds).ToList();
                var tagToAdd = model.TagIds.Except(tagInDb).ToList();

                product.ProductTags.RemoveAll(bc => tagToRemove.Contains(bc.TagId));


                foreach (var tagId in tagToAdd)
                {
                    var productTag = new ProductTag
                    {
                        TagId = tagId,
                        Product = product,
                    };

                    await _dataContext.ProductTags.AddAsync(productTag);
                }
                #endregion
            }


            IActionResult GetView(AddViewModel model)
            {

                model.Categories = _dataContext.Catagories
                   .Select(c => new CategoryListViewModel(c.Id, c.Title))
                   .ToList();

                model.CategoryIds = product.ProductCategories.Select(c => c.CategoryId).ToList();


                model.Sizes = _dataContext.Sizes
                 .Select(c => new SizeListViewModel(c.Id, c.Name))
                 .ToList();

                model.SizeIds = product.ProductSizes.Select(c => c.SizeId).ToList();



                model.Colors = _dataContext.Colors
                 .Select(c => new ColorListViewModel(c.Id, c.Name))
                 .ToList();

                model.ColorIds = product.ProductColors.Select(c => c.ColorId).ToList();



                model.Tags = _dataContext.Tags
                 .Select(c => new TagListViewModel(c.Id, c.Name))
                 .ToList();

                model.TagIds = product.ProductTags.Select(c => c.TagId).ToList();

                return View(model);
            }

        }
        #endregion



        #region Delete
        [HttpPost("delete/{id}", Name = "admin-product-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-product-list");
        }

        #endregion
    }
}

