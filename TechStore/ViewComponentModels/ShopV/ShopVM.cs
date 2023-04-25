using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.Models;

namespace TechStore.ViewComponentModels.ShopV
{
    public class ShopVM
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}
