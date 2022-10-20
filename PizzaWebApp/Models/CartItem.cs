using System.ComponentModel.DataAnnotations;

namespace PizzaWebApp.Models
{
    public class CartItem
    {
        [Key]

        public int Id { get; set; }

        public Pizza Pizza { get; set; }


    }
}
