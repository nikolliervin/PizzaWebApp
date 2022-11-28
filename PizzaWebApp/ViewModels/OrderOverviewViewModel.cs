namespace PizzaWebApp.ViewModels
{
	public class OrderOverviewViewModel
	{
		public int CartItemId { get; set; }

		public string ItemName { get; set; }

		public string ItemIngredients { get; set; }

		public double ItemPrice { get; set; }

		public int ItemAmount { get; set; }

		public double CartItemTotal { get; set; }

		public string Street { get; set; }

		public string City { get; set; }

		public string PostalCode { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public string PhoneNumber { get; set; }

	}
}
