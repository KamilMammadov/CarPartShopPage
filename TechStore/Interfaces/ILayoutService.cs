using TechStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.ViewModels.Basket;

namespace TechStore.Interfaces
{
   public interface ILayoutService
    {
        Task<Dictionary<string, string>> GetSettingsAsync();
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<List<BasketVM>> GetBasket();
        Task<IEnumerable<Product>> GetProductAsync();
    }
}
