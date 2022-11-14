using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Data;
using PizzaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaWebApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDBContext _db;

        public ShoppingCartController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            IEnumerable<CartItems> objList = _db.Cart;
            if (_db.Cart.Count() == 0)
                ViewBag.CartEmpty = "Your cart is empty";
            return View(objList);

        }


        public IActionResult SetAmount(int? id)
        {

            if (id == null || id == 0)
                return NotFound();

            var item = _db.Cart.Find(id);

            if (item == null)
                return NotFound();


            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetAmountDb(CartItems item)
        {
            var itemToBeEdited = new CartItems
            {
                Id = item.Id,
                Amount = item.Amount,
                Pizza = item.Pizza,
                PizzaName = item.PizzaName,
                PizzaIngredients = item.PizzaIngredients,
                PizzaPrice = item.PizzaPrice,
                CartItemTotal = Math.Round(item.Amount * item.PizzaPrice, 2)

            };

            if (ModelState.IsValid)
            {
                _db.Cart.Update(itemToBeEdited);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);

        }

        public IActionResult DeleteItem(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var item = _db.Cart.Find(id);

            if (item == null)
                return NotFound();

            _db.Cart.Remove(item);
            _db.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}
