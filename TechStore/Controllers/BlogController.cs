using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.DAL;
using TechStore.Models;

namespace TechStore.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.PageIndex = page;
            IEnumerable<Blog> blogs = await _context.Blogs.Where(s => s.IsDeleted == false).ToListAsync();
            if (blogs == null &&  blogs.Count() < 0)
            {
                return View("Not Found");
            }
            ViewBag.PageCount = Math.Ceiling((double)blogs.Count() / 5);

            return View(blogs.Skip((page - 1) * 5).Take(5));
        }
    }
}
