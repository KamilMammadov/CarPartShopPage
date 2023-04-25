using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.DAL;
using TechStore.ViewModels.AboutV;

namespace TechStore.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        public AboutController(AppDbContext context)
        {
                _context = context;
        }
        public async Task<IActionResult> Index()
        {
            AboutVM aboutVM = new AboutVM
            {
                Partners = await _context.Partners.Where(p=>p.IsDeleted==false).ToListAsync(),
                Posts = await _context.Posts.Where(p => p.IsDeleted == false).ToListAsync(),
                Testimonials = await _context.Testimonials.Where(p => p.IsDeleted == false).ToListAsync(),
                About = await  _context.Abouts.FirstOrDefaultAsync(a=>a.IsDeleted==false),
                Teams = await _context.Teams.Where(t=>t.IsDeleted==false).ToListAsync()

            };
            return View(aboutVM);
        }
    }
}
