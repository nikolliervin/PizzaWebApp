using Microsoft.AspNetCore.Mvc;

namespace PizzaWebApp.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
