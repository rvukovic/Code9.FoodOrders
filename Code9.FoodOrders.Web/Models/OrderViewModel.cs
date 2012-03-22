using System;
using System.Collections.Generic;
using System.Linq;
using Code9.FoodOrders.Web.Classes;

namespace Code9.FoodOrders.Web.Models
{
	public class OrderViewModel
	{
		public OrderViewModel()
		{
			OrderDate = DateTime.Now;
		}

		private Dictionary<int, string> _foodIds = new Dictionary<int, string>(); //unique food ids

		public DateTime OrderDate { get; set; }

		[ArrayNotEmptyAttribute]
		public List<int> FoodIds
		{
			get { return _foodIds.Keys.ToList(); }
		}

		public List<string> FoodNames
		{
			get { return _foodIds.Values.ToList(); }
		}

		public void AddFood(int foodId, string foodName)
		{
			if (!_foodIds.ContainsKey(foodId))
			{
				_foodIds.Add(foodId, foodName);
			}
		}

		public void RemoveFood(int foodId)
		{
			_foodIds.Remove(foodId);
		}
	}
}