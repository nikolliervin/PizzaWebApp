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



        public ShippingAddressController(ApplicationDBContext context)
        {
            _db = context;

        }
        public IActionResult Index()
        {
            var user = new AppUser();
            var currentUserId = user.Id;
            IEnumerable<ShippingAddress> shippingAddresses = _db
            .ShippingDetails.Where(s => s.UserID == currentUserId);

            if (shippingAddresses?.Any() != true)
            {
                ViewBag.NewUser = true;
            }



            return View(shippingAddresses);



        }

        public IActionResult AddAddress(ShippingAddress obj)
        {
            var user = new AppUser();

            var address = new ShippingAddress
            {
                Street = obj.Street,
                City = obj.City,
                PostalCode = obj.PostalCode,
                Name = obj.Name,
                Surname = obj.Surname,
                PhoneNumber = obj.PhoneNumber,
                UserID = user.Id
            };

            _db.ShippingDetails.Add(address);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}