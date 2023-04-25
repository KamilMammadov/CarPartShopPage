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
    public class FAQController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public FAQController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<FAQ> fAQs = await _appDbContext.FAQs.Where(f => f.IsDeleted == false).ToListAsync();
            return View(fAQs);
        }
    }
}
