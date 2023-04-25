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
    public class SpecialOfferViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public SpecialOfferViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            IEnumerable<SpecialOffer> SpecialOffers = await _context.SpecialOffers.Include(so=>so.Product).Where(so => so.IsDeleted == false).ToListAsync();
            if (SpecialOffers == null && SpecialOffers.Count() < 0)
            {
                return View("Not Found");
            }
            return View(await Task.FromResult(SpecialOffers));

        }
    }
}
