using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaWebApp.Controllers
{
	public class AdminController : Controller
	{
		private readonly ApplicationDBContext _db;
		private readonly IdentityAppContext _identity;
		private UserManager<AppUser> userManager { get; }

		private SignInManager<AppUser> signInManager { get; }

		private RoleManager<AppRole> roleManager { get; }
		public AdminController(
		ApplicationDBContext db,
		UserManager<AppUser> _userManager,
		SignInManager<AppUser> _signInManager,
		IdentityAppContext identity,
		RoleManager<AppRole> roles)
		{
			_db = db;
			userManager = _userManager;
			signInManager = _signInManager;
			_identity = identity;
			roleManager = roles;
		}

		public IActionResult Index()
		{
			var todayDate = DateTime.Now.ToString("yyyy-MM-dd");
			List<string> orderDates = _db.Orders.Select(o => o.Date.ToString()).ToList();
			List<string> bookingDates = _db.Bookings.Select(o => o.Date).ToList();
			List<double> revenue = _db.Orders.Select(o => o.Price).ToList();
			List<double> todayRevenue =
				_db.Orders
				.Where(o => o.Date
				.ToString()
				.Contains(todayDate))
				.Select(o => o.Price)
				.ToList();
			var totalProducts = _db.Pizza.Select(p => p.Id).ToList().Count;

			ViewBag.TodayOrders = CountOrderWhere(orderDates, todayDate);
			ViewBag.TotalOrders = orderDates.Count;
			ViewBag.TotalUsers = _identity.Users.Select(o => o.Id).ToList().Count;
			ViewBag.TableBookings = bookingDates.Count;
			ViewBag.TodayBookings = CountOrderWhere(bookingDates, todayDate);
			ViewBag.Revenue = revenue.Sum().ToString("0.00");
			ViewBag.RevenueToday = todayRevenue.Sum();
			ViewBag.TotalProducts = totalProducts;


			return View();
		}

		public int CountOrderWhere(List<string> orders, string date)
		{
			var counter = 0;
			foreach (var item in orders)
			{
				if (item.Contains(date))
					counter++;

			}

			return counter;
		}





		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(AppUser user)
		{
			//@TODO:Simplyfy the method
			var Message = "Your admin username or admin password was not correct";
			var adminUser = new AppUser
			{
				UserName = user.UserName,
				PasswordHash = user.PasswordHash
			};

			var userId = _identity.Users.Where(u => u.UserName == adminUser.UserName).Select(s => s.Id).ToList()[0];
			var role = _identity.UserRoles.Where(s => s.UserId == userId).Select(s => s.RoleId);

			try
			{
				var roleId = role.ToList()[0];

			}
			catch (Exception e)
			{
				ViewBag.Message = "This account is not an admin account";
				return View();
			}






			if (role.ToList()[0] == 1)
			{
				var loginResult = await signInManager.PasswordSignInAsync(adminUser.UserName, adminUser.PasswordHash, false, false);
				if (!loginResult.Succeeded)
				{
					ViewBag.Message = Message;
					return View();
				}
				else
				{
					return View("Index");
				}
			}
			else
			{
				ViewBag.Message = Message;
				return View();

			}


		}


	}
}
