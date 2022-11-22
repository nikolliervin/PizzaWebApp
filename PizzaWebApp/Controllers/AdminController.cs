using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Data;
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
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AppUser user)
        {
            var adminUser = new AppUser
            {
                UserName = user.UserName,
                PasswordHash = user.PasswordHash
            };

            var result = await userManager.FindByNameAsync(adminUser.UserName);
            var isUserAdmin = userManager.IsInRoleAsync(adminUser.Id, "Admin");
            if (result != null && isUserAdmin != null)
            {
                var loginResult = await signInManager.PasswordSignInAsync(adminUser.UserName, adminUser.PasswordHash, false, false);
                if (!loginResult.Succeeded)
                {
                    ViewBag.Message = "Your admin username or admin password was not correct";
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Message = "Your admin username or admin password was not correct";
                return View();

            }


        }


    }
}
