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
            IEnumerable<ShippingAddress> shippingAddresses = _db
            .ShippingDetails.Where(s => s.UserID == user.Id);

            if (shippingAddresses?.Any() != true)
            {
                ViewBag.NewUser = true;
                return View();
            }
            else
            {
                ViewBag.NewUser = false;
                return View(shippingAddresses);

            }





        }


        public IActionResult AddAddress()
        {
            return View();
        }

        [HttpPost]
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

            if (ModelState.IsValid)
            {
                _db.ShippingDetails.Add(address);
                _db.SaveChanges();
            }


            return View();
        }
    }
}