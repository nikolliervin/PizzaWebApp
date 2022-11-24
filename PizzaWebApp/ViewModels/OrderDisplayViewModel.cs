using PizzaWebApp.Models;

namespace PizzaWebApp.ViewModels
{
	public class OrderDisplayViewModel
	{
		public Orders Orders { get; set; }

		public ShippingAddress Address { get; set; }

		public string OrderDesc { get; set; }

		public string Street { get; set; }

		public string PhoneNumber { get; set; }

		public string User { get; set; }

		public double Price { get; set; }
	}
}
