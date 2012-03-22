using System;
namespace Code9.FoodOrders.Data
{
	public interface ICode9Service
	{
		void AddFood(Food food);
		void AddUser(string username, string password);
		void EditFood(Food food);
		System.Collections.Generic.ICollection<Food> GetAllFoods();
		Food GetFood(int id);
		System.Collections.Generic.IEnumerable<Order> GetUserOrders(string userName);
		void RemoveFood(int id);
		void StoreNewOrder(System.Collections.Generic.IList<int> foodIds, DateTime orderDate, string userName);
		bool UserFound(string username, string password);
		bool UsernameExists(string username);
	}
}
