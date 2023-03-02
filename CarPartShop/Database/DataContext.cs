using CarPartShop.Database.Models;
using CarPartShop.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CarPartShop.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
        : base(options)
        {

        }
        public DbSet<Navbar> Navbars { get; set; }
        public DbSet<SubNavbar> SubNavbars { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly<Program>();
        }
    }
}
