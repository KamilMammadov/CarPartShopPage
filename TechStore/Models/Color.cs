using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.Models
{
    public class Color : BaseEntity
    {
        [Required(ErrorMessage = "Color is Required"), StringLength(100)]
        public string Name { get; set; }
        public IEnumerable<ProductColor> ProductColors{ get; set; }

    }
}
