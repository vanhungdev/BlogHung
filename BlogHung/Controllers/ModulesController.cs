using Microsoft.AspNetCore.Mvc;

namespace BlogHung.Controllers
{
    public class ModulesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
