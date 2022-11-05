﻿using Microsoft.AspNetCore.Mvc;
using PizzaWebApp.Data;
using PizzaWebApp.Models;
using System.Collections.Generic;

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
                CartItemTotal = item.Amount * item.PizzaPrice

            };

            if (ModelState.IsValid)
            {
                _db.Cart.Update(itemToBeEdited);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);

        }
    }
}
