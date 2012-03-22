using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Code9.FoodOrders.Data
{
	public class Order
	{
		public int Id { get; set; }

		[Required]
		public DateTime OrderDate { get; set; }

		[Required]
		public User User { get; set; }

		[Required]
		public ICollection<Food> Foods { get; set; }
	}
}
