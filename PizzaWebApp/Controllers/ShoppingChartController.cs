using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Data;

namespace PizzaWebApp.Controllers
{
    public class ShoppingChartController : Controller
    {
        private readonly ApplicationDBContext _db;
        public ShoppingChartController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
