using CarPartShop.Database;
using CarPartShop.Services.Abstracts;
using CarPartShop.Services.Concretes;
using Microsoft.EntityFrameworkCore;

namespace CarPartShop.Infrastructure.Configurations
{
    public static class RegisterCustomServicesConfigurations
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserActivationService, UserActivationService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();


        }
    }
}
