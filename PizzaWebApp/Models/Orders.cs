using System;

namespace PizzaWebApp.Models
{
    public class Orders
    {
        public int Id { get; set; }

        public int PizzaId { get; set; }

        public int UserId;

        public int ShippingId { get; set; }

        public double Price { get; set; }

        public DateTime Date { get; set; }
    }
}
