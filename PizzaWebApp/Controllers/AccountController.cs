using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Data;

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
            _userManager = userManager;
            _signInManager = signInManager;
            _identity = identity;
        }

        public async IActionResult Register(AppUser newUser)
        {
            return View();

        }
    }
}
