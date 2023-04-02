using CarPartShop.Database.Models;

namespace CarPartShop.Services.Abstracts
{
    public interface IUserActivationService
    {
        Task SendActivationUrlAsync(User user);
    }
}
