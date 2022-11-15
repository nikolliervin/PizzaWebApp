using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Data;
using PizzaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<ShippingAddress> shippingAddresses = _db
            .ShippingDetails.Where(s => s.UserID == Convert.ToInt32(userId));

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var address = new ShippingAddress
            {
                Street = obj.Street,
                City = obj.City,
                PostalCode = obj.PostalCode,
                Name = obj.Name,
                Surname = obj.Surname,
                PhoneNumber = obj.PhoneNumber,
                UserID = Convert.ToInt32(userId)
            };

            if (ModelState.IsValid)
            {
                _db.ShippingDetails.Add(address);
                _db.SaveChanges();
            }


            return View();
        }

        public IActionResult Update(int? id)
        {
            var obj = _db.ShippingDetails.Find(id);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(ShippingAddress address)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ShippingAddress = new ShippingAddress
            {
                Id = address.Id,
                Street = address.Street,
                City = address.City,
                PostalCode = address.PostalCode,
                Name = address.Name,
                Surname = address.Surname,
                PhoneNumber = address.PhoneNumber,
                UserID = Convert.ToInt32(userId)

            };
            if (ModelState.IsValid)
            {
                _db.ShippingDetails.Update(ShippingAddress);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");

        }
    }
}