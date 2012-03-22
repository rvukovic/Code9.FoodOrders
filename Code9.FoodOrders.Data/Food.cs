using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Code9.FoodOrders.Data
{
	public class Food
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }

		public ICollection<Order> Orders { get; set; }
	}
}
