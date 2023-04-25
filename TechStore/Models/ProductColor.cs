using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.Models
{
    public class ProductColor
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Color Color { get; set; }
        public Nullable<int> ColorId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter number correctly")]
        public int Count { get; set; }
    }
}
