using CarPartShop.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace CarPartShop.Areas.Client.ViewComponents
{
    [Area("client")]
    [ViewComponent(Name ="Navbar")]
    public class Navbar : ViewComponent
    {
        private readonly DataContext _dataContext;

        public Navbar(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =
                _dataContext.Navbars.OrderBy(n => n.Order).ToList();

            return View(model);
        }
    }
}
