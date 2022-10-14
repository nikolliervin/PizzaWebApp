using System.ComponentModel.DataAnnotations;

namespace PizzaWebApp.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
