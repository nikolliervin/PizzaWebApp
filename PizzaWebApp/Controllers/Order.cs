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

        public IActionResult SubmitOrder()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var ShippingId = _db.ShippingDetails.Where(s => s.UserID == userId).Select(c => c.Id).ToList()[0];
            var totalPrice = Convert.ToDouble(_db.Cart.Where(c => c.UserId == Convert.ToInt32(userId)).Select(c => c.CartItemTotal).Sum());
            List<string> pizzas = _db.Cart.Where(c => c.UserId == userId).Select(c => c.PizzaName).ToList();
            List<int> amounts = _db.Cart.Where(c => c.UserId == userId).Select(c => c.Amount).ToList();
            var orderDesc = "";


            for (int i = 0; i < pizzas.Count; i++)
            {
                orderDesc += pizzas[i] + " " + amounts[i] + ", ";
            }

            var Order = new Orders
            {
                ShippingId = Convert.ToInt32(ShippingId),
                Date = DateTime.Now,
                Price = totalPrice,
                OrderDesc = orderDesc
            };

            if (ModelState.IsValid)
            {
                _db.Orders.Add(Order);
                _db.SaveChanges();
                ViewBag.Message = "Your order was confirmed successfully";

                var obj = _db.Cart.Where(c => c.UserId == userId).ToList();
                foreach (var entry in obj)
                {
                    _db.Cart.Remove(entry);
                    _db.SaveChanges();
                }

            }

            return View();
        }
    }
}
