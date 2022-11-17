using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Data;
using System.Security.Claims;

namespace PizzaWebApp.Controllers
{
    public class Order : Controller
    {
        private readonly ApplicationDBContext _db;
        public Order(ApplicationDBContext data)
        {
            _db = data;
        }
        public IActionResult Index(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.ShDetails = _db.ShippingDetails.Find(userId);



            return View();
        }
    }
}
