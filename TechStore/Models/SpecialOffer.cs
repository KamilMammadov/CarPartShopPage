
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Models
{
    public class SpecialOffer : BaseEntity
    {
        [StringLength(500)]
        [Required]
        public string Description { get; set; }

        [StringLength(500)]
        public string Image { get; set; }
        public double Discount { get; set; }

        [NotMapped]
        public IFormFile SpecialOfferFile { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
