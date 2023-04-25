using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.Models
{
    public class TechnicalSpec : BaseEntity
    {
        [StringLength(2550)]
        [Required]
        public string Height { get; set; }
        [StringLength(2550)]
        [Required]
        public string Material { get; set; }
        [StringLength(2550)]
        [Required]
        public string Case { get; set; }
        [StringLength(2550)]
        [Required]
        public string Depth { get; set; }
        [StringLength(2550)]
        [Required]
        public string Width { get; set; }

    }
}
