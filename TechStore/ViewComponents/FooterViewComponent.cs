using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.DAL;

namespace TechnoStore.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IDictionary<string, string> valuePairs)
        {
            return View(await Task.FromResult(valuePairs));
        }
    }
}
