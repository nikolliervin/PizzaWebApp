using Microsoft.AspNetCore.Mvc;

namespace PizzaWebApp.Controllers
{
    public class AdminController : Controller
    {


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
