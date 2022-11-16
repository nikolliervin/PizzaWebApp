namespace PizzaWebApp.Models
{
    public class CartItems
    {
        public int Id { get; set; }

        public Pizza Pizza { get; set; }

        public string PizzaName { get; set; }
        public string PizzaIngredients { get; set; }

        public double PizzaPrice { get; set; }
        public int Amount { get; set; }

        public double CartItemTotal { get; set; }

        public int UserId { get; set; }


    }
}
