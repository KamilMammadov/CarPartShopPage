using Microsoft.EntityFrameworkCore;

namespace CarPartShop.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
        : base(options)
        {

        }
    }
}
