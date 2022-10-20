using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Data;
using PizzaWebApp.Models;
using System.Collections.Generic;

namespace PizzaWebApp.Controllers
{
    public class ShoppingChartController : Controller
    {
        private readonly ApplicationDBContext _db;
        public ShoppingChartController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<ChartItem> objList = _db.Pizza;
            return View();
        }
    }
}
