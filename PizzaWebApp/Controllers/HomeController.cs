using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaWebApp.Data;
using PizzaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

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
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			IEnumerable<Pizza> objList = _db.Pizza;
			var count = _db.Cart.
				Where(c => c.UserId == Convert.ToInt32(userId)).Count().ToString();
			if (count == "0")
				ViewBag.CartNumber = "";
			else
				ViewBag.CartNumber = $"{count}";
			return View(objList);
		}



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[Authorize]
		[HttpPost]
		public IActionResult AddToCart(int? id)
		{

			var PizzaItem = _db.Pizza.Find(id);
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			CartItems item = new CartItems
			{
				Amount = 1,
				Pizza = PizzaItem,
				PizzaName = PizzaItem.Name,
				PizzaIngredients = PizzaItem.Ingredients,
				PizzaPrice = PizzaItem.Price,
				CartItemTotal = PizzaItem.Price,
				UserId = Convert.ToInt32(userId)

			};
			var objectExists = _db.Cart.Where(u => u.UserId == Convert.ToInt32(userId)).Select(o => o.PizzaName).ToList();
			if (objectExists.Count > 1 && objectExists[0] == item.PizzaName)
			{
				var cartItemId = Convert.ToInt32(_db.Cart.Where(p => p.PizzaName == item.PizzaName).Select(c => c.Id).ToList()[0]);
				var cartItemAmout = Convert.ToInt32(_db.Cart.Where(p => p.PizzaName == item.PizzaName).Select(c => c.Amount).ToList()[0]);
				var modifyAmount = new CartItems
				{
					Id = cartItemId,
					Pizza = PizzaItem,
					PizzaName = PizzaItem.Name,
					PizzaIngredients = PizzaItem.Ingredients,
					PizzaPrice = PizzaItem.Price,
					CartItemTotal = item.PizzaPrice * (cartItemAmout + 1),
					UserId = Convert.ToInt32(userId),
					Amount = cartItemAmout + 1
				};
				_db.Cart.Update(modifyAmount);
				_db.SaveChanges();
			}
			else
			{
				if (ModelState.IsValid)
				{
					_db.Cart.Add(item);
					_db.SaveChanges();
				}
			}



			return RedirectToAction("Index");
		}




	}
}
