using System;

namespace PizzaWebApp.Models
{
    public class Orders
    {
        public int Id { get; set; }

        public int CartId { get; set; }

        public int UserId;

        public int ShippingId { get; set; }

        public DateTime Date { get; set; }
    }
}
