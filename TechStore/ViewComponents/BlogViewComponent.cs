using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.DAL;
using TechStore.Models;

namespace TechStore.ViewComponents
{
    public class BlogViewComponent : ViewComponent
    {
        private AppDbContext _context;
        public BlogViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Blog> blog = await _context.Blogs.Where(s => s.IsDeleted == false).ToListAsync();
            if (blog == null && blog.Count() < 0)
            {
                return View("Not Found");
            }
            return View(await Task.FromResult(blog));
        }
    }
}
