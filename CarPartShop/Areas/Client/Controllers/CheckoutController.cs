using CarPartShop.Areas.Client.ViewModels;
using CarPartShop.Database;
using CarPartShop.Database.Models;
using CarPartShop.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPartShop.Areas.Client.Controllers
{
    [Area("client")]
    [Route("checkout")]
    public class CheckoutController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        private readonly IOrderService _orderService;

        public CheckoutController
            (DataContext dataContext,
            IUserService userService,
            IFileService fileService,
            IOrderService orderService
          )
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
            _orderService = orderService;

        }
        [HttpGet("order-products", Name = "client-checkout-order-products")]
        public async Task<IActionResult> OrderProducts()
        {
            var model = new OrdersProductsViewModel
            {
                Products = await _dataContext.BasketProducts
                    .Where(bp => bp.Basket!.UserId == _userService.CurrentUser.Id)
                    .Select(bp => new OrdersProductsViewModel.ItemViewModel
                    {
                        Name = bp.Product!.Name,
                        Price = bp.Product.Price,
                        Quantity = bp.Quantity,
                        Total = bp.Product.Price * bp.Quantity,
                        ColorName = bp.Color.Name,
                        SizeName = bp.Size.Name,
                        ColorId= bp.ColorId,
                        SizeId=bp.SizeId
                    }).ToListAsync(),
                Summary = new OrdersProductsViewModel.SummaryViewModel
                {
                    Total = await _dataContext.BasketProducts
                        .Where(bp => bp.Basket!.UserId == _userService.CurrentUser.Id)
                        .SumAsync(bp => bp.Product!.Price * bp.Quantity)
                }
            };

            return View(model);
        }

        [HttpPost("place-order", Name = "client-checkout-place-order")]
        public async Task<IActionResult> PlaceOrder()
        {
            var basketProducts = await _dataContext.BasketProducts
                    .Include(bp => bp.Product)
                    .Include(bp=>bp.Size)
                    .Include(bp=> bp.Color)
                    .Where(bp => bp.Basket!.UserId == _userService.CurrentUser.Id)
                    .ToListAsync();

            var order = await CreateOrderAsync();

            await CreateAndFulfillOrderProductsAsync(order, basketProducts);

            order.Total = order.OrderProducts!.Sum(op => op.Total);

            await ResetBasketAsync(basketProducts);

            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("client-home-index");



            async Task ResetBasketAsync(List<BasketProduct> basketProducts)
            {
                await Task.Run(() => _dataContext.RemoveRange(basketProducts));
            }

            async Task CreateAndFulfillOrderProductsAsync(Order order, List<BasketProduct> basketProducts)
            {
                foreach (var basketProduct in basketProducts)
                {

                    var orderProduct = new OrderProduct
                    {
                        OrderId = order.Id,
                        ProductId = basketProduct.ProductId,
                        Price = basketProduct.Product!.Price,
                        Quantity = basketProduct.Quantity,
                        Total = basketProduct.Quantity * basketProduct.Product!.Price,
                        SizeName= basketProduct.Size.Name,
                        ColorName= basketProduct.Color.Name
                        
                    };

                    await _dataContext.AddAsync(orderProduct);
                }
            }

            async Task<Order> CreateOrderAsync()
            {
                var order = new Order
                {
                    Id = await _orderService.GenerateUniqueTrackingCodeAsync(),
                    UserId = _userService.CurrentUser.Id,
                    Status = Database.Models.Enums.OrderStatus.Created,
                };

                await _dataContext.Orders.AddAsync(order);

                return order;
            }
        }
    }
}
