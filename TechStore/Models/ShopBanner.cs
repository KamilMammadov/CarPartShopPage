using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.Models
{
    public class ShopBanner : BaseEntity
    {
        [StringLength(1000)]
        public string Image { get; set; }

        [StringLength(1000)]
        [Required]
        public string Link { get; set; }

        [NotMapped]
        public IFormFile ShopBannerFile { get; set; }
    }
}
