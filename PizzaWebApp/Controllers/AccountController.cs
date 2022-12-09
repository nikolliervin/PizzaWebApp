using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Data;
using System;
using System.Threading.Tasks;

namespace PizzaWebApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly ApplicationDBContext _db;
		private readonly IdentityAppContext _identity;
		private UserManager<AppUser> userManager { get; }

		private SignInManager<AppUser> signInManager { get; }
		public AccountController(
		ApplicationDBContext db,
		UserManager<AppUser> _userManager,
		SignInManager<AppUser> _signInManager,
		IdentityAppContext identity)
		{
			_db = db;
			userManager = _userManager;
			signInManager = _signInManager;
			_identity = identity;
		}

		public IActionResult Login()
		{
			return View();
		}
		public IActionResult Register()
		{
			return View();

		}


		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Register(AppUser NewUser)
		{
			try
			{
				ViewBag.Message = "A user with that username exists or the password was not in the correct format";

				AppUser user = await userManager.FindByEmailAsync(NewUser.Email);
				if (user == null)
				{
					user = new AppUser();
					user.Email = NewUser.Email;
					user.UserName = NewUser.UserName;



					IdentityResult result = await userManager.CreateAsync(user, NewUser.PasswordHash);
					if (result.Succeeded)
					{
						await signInManager.SignInAsync(user, isPersistent: false);
						return RedirectToAction("Index", "Home");

					}
					else
						ViewBag.Error = "Account creating failed, please try again";




				}
			}

			catch (Exception ex)
			{

				ViewBag.Message = ex.Message;
			}

			return View();
		}

		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Login");
		}


		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Login(AppUser user)
		{
			var login = new AppUser
			{
				UserName = user.UserName,
				PasswordHash = user.PasswordHash,
			};

			if (!User.Identity.IsAuthenticated)
			{
				var result = await userManager.FindByNameAsync(login.UserName);
				if (result == null)
				{
					ViewBag.Error = "An account with that username was not found";

					return View();

				}
				else
				{
					var loginResult = await signInManager.PasswordSignInAsync(login.UserName, login.PasswordHash, false, false);
					if (!loginResult.Succeeded)
					{
						ViewBag.Error = "Your username or password was incorrect";
						return View();
					}

				}

			}


			return RedirectToAction("Index", "Home");

		}

	}
}
