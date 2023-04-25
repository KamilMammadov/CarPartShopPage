using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.Models
{
    public class Term : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [Required]
        [StringLength(1055)]
        public string Description { get; set; }
    }
}
