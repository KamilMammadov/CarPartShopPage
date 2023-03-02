using CarPartShop.Database.Models.Common;

namespace CarPartShop.Database.Models
{
    public class Navbar: BaseEntity
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public string URL { get; set; }
        public List<SubNavbar>? SubNavbars { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
