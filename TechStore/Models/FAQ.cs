using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.Models
{
    public class FAQ : BaseEntity
    {
        [StringLength(255)]
        [Required]
        public string Question { get; set; }
        [StringLength(1055)]
        [Required]
        public string Answer { get; set; }
    }
}
