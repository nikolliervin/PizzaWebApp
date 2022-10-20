using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Data;
using PizzaWebApp.Models;

namespace PizzaWebApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDBContext _db;
        public ShoppingCartController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddToCart(CartItem obj)
        {
            if (ModelState.IsValid)
            {
                _db.ChartItems.Add(obj);
                _db.SaveChanges();
            }

            return View("Home/Index");
        }
    }
}
