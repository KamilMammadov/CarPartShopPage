﻿using CarPartShop.Areas.Client.ViewModels;
using CarPartShop.Contracts.File;
using CarPartShop.Database;
using CarPartShop.Database.Models;
using CarPartShop.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CarPartShop.Services.Concretes
{
    public class BasketService : IBasketService
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;


        public BasketService(DataContext dataContext, IUserService userService, IHttpContextAccessor httpContextAccessor,IFileService fileService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
        }


        public async Task<List<ProductCookieViewModel>> AddBasketProductAsync(Product product,ProductCookieViewModel model)
        {
            if (_userService.IsAuthenticated)
            {
                await AddToDatabaseAsync();

                return new List<ProductCookieViewModel>();
            }

            return AddToCookie();





            //Add product to database if user is authenticated
            async Task AddToDatabaseAsync()
            {
                var basketProduct = await _dataContext.BasketProducts
                    .FirstOrDefaultAsync(bp => bp.Basket.UserId == _userService.CurrentUser.Id && bp.ProductId == product.Id);
                if (basketProduct is not null)
                {
                    basketProduct.Quantity++;
                }
                else
                {
                    var basket = await _dataContext.Baskets.FirstAsync(b => b.UserId == _userService.CurrentUser.Id);

                    basketProduct = new BasketProduct
                    {
                        Quantity = model.Quantity,
                        BasketId = basket.Id,
                        ProductId = product.Id,
                        SizeId=model.SizeId,
                        ColorId=model.ColorId
                    };

                    await _dataContext.BasketProducts.AddAsync(basketProduct);
                }

                await _dataContext.SaveChangesAsync();
            }
            

            //Add product to cookie if user is not authenticated 
            List<ProductCookieViewModel> AddToCookie()
            {
                var productCookieValue = _httpContextAccessor.HttpContext.Request.Cookies["products"];
                var productsCookieViewModel = productCookieValue is not null
                    ? JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue)
                    : new List<ProductCookieViewModel> { };

                var productCookieViewModel = productsCookieViewModel!.FirstOrDefault(pcvm => pcvm.Id == product.Id 
                && pcvm.SizeId==model.SizeId && pcvm.ColorId == model.ColorId);
                if (productCookieViewModel is null)
                {
                    productsCookieViewModel
                        !.Add(new ProductCookieViewModel(product.Id, product.Name,
                         product.ProductImages.Take(1).FirstOrDefault() != null
                   ? _fileService.GetFileUrl(product.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                   : String.Empty , model.Quantity, product.Price, product.Price,model.ColorId,model.SizeId));
                }
                else
                {
                    productCookieViewModel.Quantity += model.Quantity;
                    productCookieViewModel.Total = productCookieViewModel.Quantity * productCookieViewModel.Price;
                }

                _httpContextAccessor.HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productsCookieViewModel));

                return productsCookieViewModel;
            }
        }

    
    }
}