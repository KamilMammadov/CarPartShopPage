using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using TechStore.DAL;
using TechStore.Interfaces;
using TechStore.Models;
using TechStore.Services;

namespace TechStore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddIdentity<AppUser, IdentityRole>(options =>
             {
                 options.User.RequireUniqueEmail = true;

                 options.Password.RequireDigit = true;
                 options.Password.RequiredLength = 8;
                 options.Password.RequireLowercase = true;
                 options.Password.RequireNonAlphanumeric = true;
                 options.Password.RequireUppercase = true;

                 options.Lockout.AllowedForNewUsers = true;
                 options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                 options.Lockout.MaxFailedAccessAttempts = 3;
             }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

            services.AddSession(option => {
                option.IdleTimeout = TimeSpan.FromSeconds(10);
            });
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/PageNotFound");
            }
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Error/PageNotFound";
                    await next();
                }
            });
           

            app.UseRouting();
            app.UseSession();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}"
                   );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=home}/{action=index}/{id?}"
                    );
            });
        }
    }
}
