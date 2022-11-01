using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaWebApp.Data;
using PizzaWebApp.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace PizzaWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Pizza> objList = _db.Pizza;
            return View(objList);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public IActionResult AddToCart(int? id)
        {
            var PizzaItem = _db.Pizza.Find(id);
            CartItems item = new CartItems { Amount = 1, Pizza = PizzaItem };

            if (ModelState.IsValid)
            {
                _db.Cart.Add(item);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


    }
}
