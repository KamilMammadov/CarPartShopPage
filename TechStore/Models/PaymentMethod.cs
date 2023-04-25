using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.Models
{
    public class PaymentMethod : BaseEntity
    {
        [StringLength(500)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        [Required]
        public string Description { get; set; }

        [StringLength(1000)]
        public string MainImage { get; set; }

        [NotMapped]
        public IFormFile PaymentImageFile { get; set; }
    }
}
