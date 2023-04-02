using CarPartShop.Areas.Client.ViewModels;
using CarPartShop.Database;
using CarPartShop.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPartShop.Areas.Client.Controllers
{
    [Area("client")]
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public AccountController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }


        [HttpGet("dashboard", Name = "client-account-dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
        [HttpGet("order", Name = "client-account-order")]
        public async  Task<IActionResult> Order()
        {
            var model = await _dataContext.Orders.Include(o => o.OrderProducts)
                .Where(o => o.UserId == _userService.CurrentUser.Id)
                .Select(o => new OrderViewModel(o.Id, o.CreatedAt, o.Status,o.OrderProducts.Count ,o.Total))
                .ToListAsync();
            return View(model);
        }

        [HttpGet("detail", Name = "client-account-detail")]
        public  async Task<IActionResult> Detail()
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == _userService.CurrentUser.Id);

            var model = new UserViewModel
            {
                Email = user.Email,
                CurrentPasword = null,
                Password = null,
                ConfirmPassword = null,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return View(model);
        }
        [HttpPost("detail", Name = "client-account-detail")]
        public async Task<IActionResult> Detail(UserViewModel newuser)
        {
            if (!ModelState.IsValid)
            {
                return View(newuser);
            }

            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == _userService.CurrentUser.Id);
           
            if (user is null)
            {
                return NotFound();
            }

            if (newuser.CurrentPasword!=user.Password)
            {
                return View(newuser);
            }


            user.FirstName = newuser.FirstName;
            user.LastName=newuser.LastName;
            user.Email = newuser.Email;
            user.Password=newuser.Password;

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-account-dashboard");
        }

    }
}
