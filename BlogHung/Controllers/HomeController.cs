using BlogHung.Infrastructure.Hosting.Middlewares;
using BlogHung.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogHung.Controllers
{
    [ServiceFilter(typeof(LogModelDataAttribute))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int id = 1)
        {
         /*   LoggingHelper.SetProperty("ResponseData", "123!");*/
            return View();
        }

        [HttpPost]
        public IActionResult Privacy(Users users)
        {
            var user = new Users();
            user.Id = 1;
            user.Name = "Hung";
            return View(user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}