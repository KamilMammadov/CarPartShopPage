using CarPartShop.Database.Common;

namespace CarPartShop.Database.Models
{
    public class Navbar: BaseEntity,IAuditable
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public string URL { get; set; }
        public List<SubNavbar>? subNavbars { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
