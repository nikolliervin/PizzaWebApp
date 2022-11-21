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


        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            //var user = new AppUser();
            //user.Email = "admin@pizza.com";
            //user.UserName = "admin";

            var admin = new AppRole()
            {
                Name = "Admin",
                NormalizedName = "ADMIN",

            };
            var user = _identity.Users.Find(18);
            //await roleManager.CreateAsync(admin);
            await userManager.AddToRoleAsync(user, "Admin");






            return View();
        }
    }
}
