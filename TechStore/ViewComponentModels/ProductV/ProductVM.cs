using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.Models;

namespace TechStore.ViewComponentModels.ProductV
{
    public class ProductVM
    {
        public IEnumerable<Product> NewArrivals { get; set; }
        public IEnumerable<Product> BestSellers { get; set; }
        public IEnumerable<Product> Featured { get; set; }

    }
}
