using System;

namespace PizzaWebApp.ViewModels
{
	public class OrderDisplayViewModel
	{
		public string OrderDesc { get; set; }

		public string Street { get; set; }

		public string PhoneNumber { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }
		public double Price { get; set; }

		public DateTime Date { get; set; }


	}
}
