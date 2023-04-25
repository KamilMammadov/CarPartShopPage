using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.Models
{
    public class Partner : BaseEntity
    {
        [StringLength(2550)]
        public string Image { get; set; }

        [StringLength(2550)]
        [Required]
        public string Link { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
}
