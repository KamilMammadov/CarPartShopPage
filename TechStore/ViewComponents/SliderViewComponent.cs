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
    public class SliderViewComponent : ViewComponent
    {
        public AppDbContext _context { get; }
        public SliderViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Slider> sliders = await _context.Sliders.Where(s => s.IsDeleted == false).ToListAsync();
            if (sliders == null && sliders.Count() < 0)
            {
                return View("Not Found");
            }
            return View(await Task.FromResult(sliders));
        }

    }
}
