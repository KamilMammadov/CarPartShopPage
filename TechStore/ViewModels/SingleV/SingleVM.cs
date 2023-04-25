using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.Models;

namespace TechStore.ViewModels.SingleV
{
    public class SingleVM
    {
       public IEnumerable<Product> RecentProduct { get; set; }
        public IEnumerable<Product> Product { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }

        public Product ProductSingle { get; set; }
        public IEnumerable<Description> Descriptions { get; set; }




    }
}
