using System.ComponentModel.DataAnnotations;

namespace PizzaWebApp.Models
{
    public class ShippingAddress
    {
        [Key]
        public int Id { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public int UserID { get; set; }


    }
}
