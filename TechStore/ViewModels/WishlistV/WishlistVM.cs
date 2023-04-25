using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.ViewModels.WishlistV
{
    public class WishlistVM
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
    }
}
