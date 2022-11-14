using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Data;
using PizzaWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace PizzaWebApp.Controllers
{
    public class ShippingAddressController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly AppUser _user;


        public ShippingAddressController(ApplicationDBContext context, AppUser user)
        {
            _db = context;
            _user = user;
        }
        public IActionResult Index()
        {
            var CurrentUserID = _user.Id;
            IEnumerable<ShippingAddress> shippingAddresses = _db
            .ShippingDetails
            .Where(s => s.UserID == CurrentUserID);

            if (shippingAddresses == null)
                ViewBag.Message = "You don't have a shipping Address";

            return View(shippingAddresses);
        }
    }
}
