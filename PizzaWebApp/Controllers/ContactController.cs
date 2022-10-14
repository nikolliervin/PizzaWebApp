using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Data;
using PizzaWebApp.Models;

namespace PizzaWebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDBContext _db;

        public ContactController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Contact obj)
        {


            if (ModelState.IsValid)
            {
                _db.Add(obj);
                _db.SaveChanges();
                ViewBag.SuccessMessage = "Form was submited successfully!";
                ModelState.Clear();


            }

            return View("Index");
        }


    }
}
