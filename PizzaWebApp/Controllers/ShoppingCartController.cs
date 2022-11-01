using Microsoft.AspNetCore.Mvc;

namespace PizzaWebApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
