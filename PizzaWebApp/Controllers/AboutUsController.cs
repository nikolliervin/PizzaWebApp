using Microsoft.AspNetCore.Mvc;

namespace PizzaWebApp.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
