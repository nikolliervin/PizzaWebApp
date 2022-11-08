using System.ComponentModel.DataAnnotations;

namespace PizzaWebApp.Models
{
    public class ShippingDetails
    {
        [Key]
        public int ShippingId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Street { get; set; }

        public string Street2 { get; set; }

        public string City { get; set; }

        public int ZipCode { get; set; }

        public string PhoneNumber { get; set; }
    }
}
