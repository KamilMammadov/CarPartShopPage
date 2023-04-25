using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.Models;

namespace TechStore.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SpecialOffer> SpecialOffers { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ShopBanner> ShopBanners { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<LeaveUsReply> LeaveUsReplies { get; set; }
        public DbSet<CategorySlider> CategorySliders { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<TechnicalSpec> TechnicalSpecs { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Term> Terms { get; set; }

        public DbSet<Wishlist> Wishlists { get; set; }


    }
}
