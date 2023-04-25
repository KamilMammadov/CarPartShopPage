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
    public class TermController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public TermController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Term> terms = await _appDbContext.Terms.Where(t => t.IsDeleted == false).ToListAsync();
            return View(terms);
        }
    }
}
