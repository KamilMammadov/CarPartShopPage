﻿using CarPartShop.Database;
using CarPartShop.Services.Abstracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Org.BouncyCastle.Bcpg;
using System.Security.Claims;
using CarPartShop.Database.Models;
using Microsoft.EntityFrameworkCore;
using CarPartShop.Areas.Client.ViewModels;
using CarPartShop.Contracts.Identity;
using System.Text.Json;

namespace CarPartShop.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserActivationService _userActivationService;
        private User _currentUser;

        public UserService(
            DataContext dataContext,
            IHttpContextAccessor httpContextAccessor,
            IUserActivationService userActivationService)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _userActivationService = userActivationService;
        }

        public bool IsAuthenticated
        {
            get => _httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated;
        }

        public User CurrentUser
        {
            get
            {
                if (_currentUser is not null)
                {
                    return _currentUser;
                }

                var idClaim = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(C => C.Type == CustomClaimNames.ID);
                if (idClaim is null)
                    throw new Exception();

                _currentUser = _dataContext.Users.First(u => u.Id == Int32.Parse(idClaim.Value));

                return _currentUser;
            }
        }

        public async Task<bool> CheckEmailConfirmedAsync(string? email)
        {
            return await _dataContext.Users.AnyAsync(u => u.Email == email && u.IsEmailConfirmed);
        }


        public string GetCurrentUserFullName()
        {
            return $"{CurrentUser.FirstName} {CurrentUser.LastName}";
        }

        public async Task<bool> CheckPasswordAsync(string? email, string? password)
        {
            return await _dataContext.Users.AnyAsync(u => u.Email == email && u.Password == password);
        }

        public async Task SignInAsync(int id, string? role = null)
        {
            var claims = new List<Claim>
            {
                new Claim(CustomClaimNames.ID, id.ToString())
            };

            if (role is not null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
        }

        public async Task SignInAsync(string? email, string? password, string? role = null)
        {
            var user = await _dataContext.Users.FirstAsync(u => u.Email == email && u.Password == password);
            await SignInAsync(user.Id, role);
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task CreateAsync(RegisterViewModel model)
        {
            var user = await CreateUserAsync();
            
            var basket = await CreateBasketAsync();

            await CreteBasketProductsAsync();

            await _userActivationService.SendActivationUrlAsync(user);

            await _dataContext.SaveChangesAsync();


            async Task<User> CreateUserAsync()
            {
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                };
                await _dataContext.Users.AddAsync(user);

                return user;
            }

            async Task<Basket> CreateBasketAsync()
            {
                //Create basket process
                var basket = new Basket
                {
                    User = user,
                };
                await _dataContext.Baskets.AddAsync(basket);

                return basket;
            }

            async Task CreteBasketProductsAsync()
            {
                //Add products to basket if cookie exists
                var productCookieValue = _httpContextAccessor.HttpContext!.Request.Cookies["products"];
                if (productCookieValue is not null)
                {
                    var productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue);
                    foreach (var productCookieViewModel in productsCookieViewModel)
                    {
                        var book = await _dataContext.Products.FirstOrDefaultAsync(b => b.Id == productCookieViewModel.Id);
                        var basketProduct = new BasketProduct
                        {
                            Basket = basket,
                            ProductId = book!.Id,
                            Quantity = productCookieViewModel.Quantity,
                        };

                        await _dataContext.BasketProducts.AddAsync(basketProduct);
                    }
                }
            }

        }
    }
}
