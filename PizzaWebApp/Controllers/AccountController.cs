﻿using Microsoft.AspNetCore.Identity;
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
                ViewBag.Message = "User already exists";
                AppUser user = await userManager.FindByEmailAsync(NewUser.Email);
                if (user == null)
                {
                    user = new AppUser();
                    user.Email = NewUser.Email;
                    user.UserName = NewUser.UserName;
                    IdentityResult result = await userManager.CreateAsync(user, NewUser.PasswordHash);
                    if (result.Succeeded)
                    {
                        ViewBag.Success = "User was created";
                        await signInManager.SignInAsync(user, isPersistent: false);
                    }
                    else
                        ViewBag.Error = result.Errors;




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
            return RedirectToAction("Index", "Home", false);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> LogUserIn(AppUser user)
        {
            if (!User.Identity.IsAuthenticated)
            {
                var result = await userManager.FindByNameAsync(user.UserName);
                if (result == null)
                {
                    ViewBag.Error = "Your password or username was not correct";

                }
                else
                {
                    await signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, false, false);

                }
            }


            return View();

        }

    }
}
