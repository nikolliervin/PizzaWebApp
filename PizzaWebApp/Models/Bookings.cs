using System.ComponentModel.DataAnnotations;

namespace PizzaWebApp.Models
{
    public class Bookings
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public int NrOfPeople { get; set; }

        public string Message { get; set; }
    }
}
