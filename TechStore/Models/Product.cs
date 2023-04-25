using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.Models
{
    public class Product : BaseEntity
    {
        [StringLength(500)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        [Required]
        public string infoText { get; set; }

        [Column(TypeName = "money")]
        [Required]
        public double Price { get; set; }

        [Required]
        public double DiscountedPrice { get; set; }

        [StringLength(3)]
        public string Seria { get; set; }

        public int Code { get; set; }

        public int Count { get; set; }

        [StringLength(1000)]
        public string MainImage { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Brand Brand { get; set; }
        public Nullable<int> TagId { get; set; }
        public Tag Tag { get; set; }
        public Nullable<int> TechnicalSpecId { get; set; }
        public TechnicalSpec TechnicalSpec { get; set; }
        [NotMapped]
        public List<int> ColorIds { get; set; } = new List<int>();
        public List<ProductColor> ProductColors { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<Description> Descriptions { get; set; }

        [NotMapped]
        public IFormFile[] ProductImagesFile { get; set; }

        [NotMapped]
        public List<int> Counts { get; set; } = new List<int>();
        [NotMapped]
        public IFormFile ProductFile { get; set; }
        public bool IsNewArrival { get; set; }
        public bool IsBestSeller { get; set; }
        public bool IsFeatured { get; set; }
    }
}
