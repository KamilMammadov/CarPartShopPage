using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.Models
{
    public class Description : BaseEntity
    {
        [StringLength(2550)]
        [Required]
        public string Title { get; set; }

        [StringLength(2550)]
        [Required]
        public string DescriptionGeneral { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
