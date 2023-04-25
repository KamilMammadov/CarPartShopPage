using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.Models
{
    public class CategorySlider : BaseEntity
    {
        [StringLength(400)]
        [Required]
        public string Title { get; set; }
        [StringLength(400)]
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile CategorySliderImage { get; set; }

    }
}
