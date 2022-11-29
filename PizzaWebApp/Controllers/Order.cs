using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Data;
using PizzaWebApp.Models;
using PizzaWebApp.ViewModels;
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

			ViewBag.ShDetails = _db.ShippingDetails.Where(u => u.UserID == Convert.ToInt32(userId)).ToList()[0];
			ViewBag.Subtotal = _db.Cart.Where(u => u.UserId == Convert.ToInt32(userId)).Select(c => c.CartItemTotal).ToList().Sum().ToString("0.00");
			IEnumerable<CartItems> cartItems = _db.Cart.Where(u => u.UserId == Convert.ToInt32(userId));
			return View(cartItems);
		}

		public IActionResult MyOrders()
		{

			return View();
		}
		public List<OrderDisplayViewModel> OrderAddress(int userId)
		{

			var query = (from s in _db.ShippingDetails
						 join o in _db.Orders on
					   s.Id equals o.ShippingId
						 where s.UserID == userId
						 select new OrderDisplayViewModel
						 {
							 Name = s.Name,
							 Surname = s.Surname,
							 PhoneNumber = s.PhoneNumber,
							 Street = s.Street,
							 OrderDesc = o.OrderDesc,
							 Price = o.Price,
							 Date = o.Date,
						 }).ToList();
			return query;
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
