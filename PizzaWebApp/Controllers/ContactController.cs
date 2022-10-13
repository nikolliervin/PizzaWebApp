using Microsoft.AspNetCore.Mvc;

namespace PizzaWebApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
