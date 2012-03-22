using System.ComponentModel.DataAnnotations;

namespace Code9.FoodOrders.Data
{
	public class User
	{
		public int Id { get; set; }

		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
