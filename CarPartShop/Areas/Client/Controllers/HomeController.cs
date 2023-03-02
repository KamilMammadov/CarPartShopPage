using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarPartShop.Areas.Client.Controllers
{
    [Area("client")]
    public class HomeController : Controller
    {
       
        [HttpGet("/", Name = "client-home-index")]
        [HttpGet("index")]
        public ActionResult Index()
        {
            return View();
        }

      
    }
}
