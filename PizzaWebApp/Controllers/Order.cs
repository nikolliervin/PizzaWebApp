using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Data;
using PizzaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ViewBag.ShDetails = _db.ShippingDetails.Find(Convert.ToInt32(userId));
            IEnumerable<CartItems> cartItems =
                _db.Cart.Where(c => c.UserId == Convert.ToInt32(userId));
            ViewBag.Subtotal = cartItems.Select(c => c.CartItemTotal).Sum();


            return View(cartItems);
        }
    }
}
