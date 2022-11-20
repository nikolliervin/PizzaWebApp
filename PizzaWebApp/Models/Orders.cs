using System;

namespace PizzaWebApp.Models
{
    public class Orders
    {
        public int Id { get; set; }

        public int ShippingId { get; set; }
        public string OrderDesc { get; set; }

        public double Price { get; set; }

        public DateTime Date { get; set; }
    }
}
