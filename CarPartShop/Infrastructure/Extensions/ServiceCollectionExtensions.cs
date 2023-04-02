using AspNetCore.IServiceCollection.AddIUrlHelper;
using CarPartShop.Database;
using CarPartShop.Infrastructure.Configurations;
using CarPartShop.Options;
using CarPartShop.Services.Abstracts;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace CarPartShop.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                  .AddCookie(o =>
                  {
                      o.Cookie.Name = "Identity";
                      o.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                      o.LoginPath = "/auth/login";
                      o.AccessDeniedPath = "/admin/auth/login";
                  });

            services.AddHttpContextAccessor();

            services.AddUrlHelper();


            services.ConfigureMvc();

            services.ConfigureDatabase(configuration);

            services.ConfigureOptions(configuration);

            services.ConfigureFluentValidatios(configuration);

            services.RegisterCustomServices(configuration);
        }
    }
}
