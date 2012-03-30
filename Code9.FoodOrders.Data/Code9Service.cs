using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Code9.FoodOrders.Data
{
	public class Code9Service : ICode9Service
	{
		public Code9Entities _db;
		public Code9Service(Code9Entities dbContext)
		{
			_db = dbContext;
		}

		public void AddUser(string username, string password)
		{
			_db.Users.Add(new User
							{
								Username = username,
								Password = password
							});

			_db.SaveChanges();
		}

		public bool UsernameExists(string username)
		{
			return _db.Users.Any(u => u.Username == username);
		}

		public bool UserFound(string username, string password)
		{
			return _db.Users.Any(u => u.Username == username &&
									 u.Password == password);

		}

		public ICollection<Food> GetAllFoods()
		{
			return _db.Foods.ToList();
		}

		public Food GetFood(int id)
		{
			return _db.Foods.Find(id);
		}

		public void RemoveFood(int id)
		{
			var food = _db.Foods.Find(id);
			if ( food != null )
			{
				_db.Foods.Remove(food);
				_db.SaveChanges();
			}
		}

		public void AddFood(Food food)
		{
			_db.Foods.Add(food);
			_db.SaveChanges();
		}

		public void EditFood(Food food)
		{
			var dbFood = _db.Foods.Find(food.Id);
			if ( dbFood != null )
			{
				_db.Entry(dbFood).CurrentValues.SetValues(food);
				_db.SaveChanges();
			}
		}

		public void StoreNewOrder(IList<int> foodIds, DateTime orderDate, string userName)
		{
			Order newOrder = new Order();
			newOrder.OrderDate = orderDate;

			User user = _db.Users.First(u => u.Username == userName);
			newOrder.User = user;
			_db.Entry(user).State = EntityState.Unchanged;
			List<Food> foodsToAttach = new List<Food>();
			foreach ( var foodId in foodIds )
			{
				var f = _db.Foods.Find(foodId);
				_db.Entry(f).State = EntityState.Unchanged;
				foodsToAttach.Add(f);
			}

			newOrder.Foods = foodsToAttach;
			_db.Orders.Add(newOrder);
			_db.SaveChanges();
		}

		public IEnumerable<Order> GetUserOrders(string userName)
		{
			User user = _db.Users.First(u => u.Username == userName);
			var retVal = _db.Orders.Where(o => o.User.Username == userName).ToList();
			foreach ( var r in retVal )
			{
				_db.Entry(r).Collection(a => a.Foods).Load();
			}

			return retVal;
		}
	}
}
