using CarPartShop.Areas.Client.ViewModels;
using CarPartShop.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace CarPartShop.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopPageCategory")]

    public class ShopPageCategory: ViewComponent
    {
        private readonly DataContext _dataContext;

        public ShopPageCategory(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Catagories.Select(c => new CategoryListItemViewModel(c.Id, c.Title)).ToListAsync();

            return View(model);
        }
    }
}
