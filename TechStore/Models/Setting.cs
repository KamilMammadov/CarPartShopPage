using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.Models
{
    public class Setting : BaseEntity
    {
        [StringLength(4000)]
        public string Key { get; set; }
        [StringLength(4000)]
        public string Value { get; set; }
        [NotMapped]
        public IFormFile LogoImg { get; set; }

        [NotMapped]
        public IFormFile Paypal { get; set; }

        [NotMapped]
        public IFormFile Discover { get; set; }

        [NotMapped]
        public IFormFile Visa { get; set; }

        [NotMapped]
        public IFormFile MasterCard { get; set; }
        [NotMapped]
        public IFormFile AmericanExpress { get; set; }
        [NotMapped]
        public IFormFile FooterImg { get; set; }
    }
}
