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
    public class TagViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public TagViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Tag> tags = await _context.Tags.Where(s => s.IsDeleted == false).ToListAsync();
            if (tags == null && tags.Count() < 0)
            {
                return View("Not Found");
            }
            return View(await Task.FromResult(tags));
        }
    }
}
