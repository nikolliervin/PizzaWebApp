using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Data;
using PizzaWebApp.Models;
using System.Collections.Generic;

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

            IEnumerable<CartItems> objList = _db.Cart;

            return View(objList);

        }
    }
}
