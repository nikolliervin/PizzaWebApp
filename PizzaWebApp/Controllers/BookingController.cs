using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Data;
using PizzaWebApp.Models;

namespace PizzaWebApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDBContext _db;

        public BookingController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Bookings obj)
        {
            if (ModelState.IsValid)
            {
                _db.Add(obj);
                _db.SaveChanges();
                ViewBag.SuccessMessage = "Your booking was set successfully! You will be called to confirm your booking.";

            }
            return View("Index");
        }
    }
}
