using CarPartShop.Areas.Admin.ViewModels.Blog;
using CarPartShop.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPartShop.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/BlogProduct")]
    public class BlogController : Controller
    {
        private readonly DataContext _dataContext;


        public BlogController(DataContext dataContext)
        {
            _dataContext = dataContext;

        }

        [HttpGet("list", Name = "admin-blog-list")]
        public async Task<IActionResult> ListAsync() 
        {
            var model = await _dataContext.Blogs.Select(p => new ListItemViewModel(p.Id,
                p.Title,p.Content,p.From,p.CreatedAt,
                p.BlogandBlogCategories.Select(pc => pc.BlogCategory).Select(c => new CategoryViewModel(c.Title, c.Parent.Title)).ToList(),
                p.blogandBlogTags.Select(pt => pt.BlogTag).Select(t => new TagViewModel(t.Name)).ToList()
                )).ToListAsync();

            return View(model);
        }

    }
}
