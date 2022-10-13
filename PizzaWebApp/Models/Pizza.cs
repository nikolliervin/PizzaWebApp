using System.ComponentModel.DataAnnotations;

namespace PizzaWebApp.Models
{
    public class Pizza
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Ingredients { get; set; }
    }
}
