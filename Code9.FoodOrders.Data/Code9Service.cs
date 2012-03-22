using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Code9.FoodOrders.Data
{
	public class Code9Service
	{
		public void AddUser(string username, string password)
		{
			using (var db = new Code9Entities())
			{
				db.Users.Add(new User
				             	{
				             		Username = username,
				             		Password = password
				             	});

				db.SaveChanges();
			}
		}

		public bool UsernameExists(string username)
		{
			using (var db = new Code9Entities())
			{
				return db.Users.Any(u => u.Username == username);
			}
		}

		public bool UserFound(string username, string password)
		{
			using (var db = new Code9Entities())
			{

				return db.Users.Any(u => u.Username == username &&
				                         u.Password == password);
			}
		}

		public ICollection<Food> GetAllFoods()
		{
			using (var db = new Code9Entities())
			{
				return db.Foods.ToList();
			}
		}

		public Food GetFood(int id)
		{
			using (var db = new Code9Entities())
			{
				return db.Foods.Find(id);
			}
		}

		public void RemoveFood(int id)
		{
			using (var db = new Code9Entities())
			{
				var food = db.Foods.Find(id);
				if (food != null)
				{
					db.Foods.Remove(food);
					db.SaveChanges();
				}
			}
		}

		public void AddFood(Food food)
		{
			using (var db = new Code9Entities())
			{
				db.Foods.Add(food);
				db.SaveChanges();
			}
		}

		public void EditFood(Food food)
		{
			using (var db = new Code9Entities())
			{
				var dbFood = db.Foods.Find(food.Id);
				if (dbFood != null)
				{
					db.Entry(dbFood).CurrentValues.SetValues(food);
					db.SaveChanges();
				}
			}
		}

		public void StoreNewOrder(IList<int> foodIds, DateTime orderDate, string userName)
		{
			Order newOrder = new Order();
			newOrder.OrderDate = orderDate;
			using (var db = new Code9Entities())
			{
				User user = db.Users.First(u => u.Username == userName);
				newOrder.User = user;
				db.Entry(user).State = EntityState.Unchanged;
				List<Food> foodsToAttach = new List<Food>();
				foreach (var foodId in foodIds)
				{
					var f = db.Foods.Find(foodId);
					db.Entry(f).State = EntityState.Unchanged;
					foodsToAttach.Add(f);
				}

				newOrder.Foods = foodsToAttach;
				db.Orders.Add(newOrder);
				db.SaveChanges();
			}
		}

		public IEnumerable<Order> GetUserOrders(string userName)
		{
			using (var db = new Code9Entities())
			{
				User user = db.Users.First(u => u.Username == userName);
				var retVal = db.Orders.Where(o => o.User.Username == userName).ToList();
				foreach (var r in retVal)
				{
					db.Entry(r).Collection(a => a.Foods).Load();
				}

				return retVal;
			}
		}
	}
}
