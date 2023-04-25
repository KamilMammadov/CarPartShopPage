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
    public class ServicesViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public ServicesViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            IEnumerable<PaymentMethod> paymentMethods = await _context.PaymentMethod.Where(c => c.IsDeleted == false).ToListAsync();
            if (paymentMethods == null && paymentMethods.Count() < 0)
            {
                return View("Not Found");
            }
            return View(await Task.FromResult(paymentMethods));
        }

    }
}
