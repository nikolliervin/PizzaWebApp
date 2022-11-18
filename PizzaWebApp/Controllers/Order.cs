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

        public IActionResult SubmitOrder(CartItems item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ShippingId = _db.ShippingDetails.Where(s => s.UserID == Convert.ToInt32(User)).Select(u => u.Id);
            var totalPrice = Convert.ToDouble(_db.Cart.Where(c => c.UserId == Convert.ToInt32(userId)).Select(c => c.CartItemTotal).Sum());

            var order = new Orders
            {
                PizzaId = Convert.ToInt32(item.Pizza),
                ShippingId = Convert.ToInt32(ShippingId),
                Date = DateTime.Now,
                Price = totalPrice

            };
            if (ModelState.IsValid)
            {
                _db.Orders.Add(order);
                _db.SaveChanges();
                ViewBag.Message = "Your order was confirmed. You will be contacted by us soon";
            }
            else
            {
                ViewBag.Message = "There was a problem while confirming the order";
            }
            return View();
        }
    }
}
