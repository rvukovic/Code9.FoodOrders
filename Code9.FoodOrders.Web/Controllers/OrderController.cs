using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Code9.FoodOrders.Data;
using Code9.FoodOrders.Web.Classes;
using Code9.FoodOrders.Web.Models;

namespace Code9.FoodOrders.Web.Controllers
{
	[Authorize]
	public class OrderController : Controller
	{
		private ICode9Service _service;
		public OrderController(ICode9Service service)
		{
			_service = service;
		}

		public ActionResult CompleteOrder()
		{
			var order = CurrentSession.CurrentOrder;

			if (TryValidateModel(order))
			{
				_service.StoreNewOrder(order.FoodIds, DateTime.Now, User.Identity.Name);
				CurrentSession.ClearOrder();
				return View("CompleteOrderStatus", order);
			}
			
			return Redirect(Request.UrlReferrer.AbsolutePath);
		}

		public ActionResult RemoveOrderFood(int foodId)
		{
			CurrentSession.CurrentOrder.RemoveFood(foodId);
			return Redirect(Request.UrlReferrer.AbsolutePath);
		}


		public ActionResult Index()
		{
			var viewModels = new List<OrderViewModel>();
			var orders = _service.GetUserOrders(User.Identity.Name);

			foreach (var oneOrder in orders)
			{
				var o = new OrderViewModel
				        	{
				        		OrderDate = oneOrder.OrderDate
				        	};

				foreach (var food in oneOrder.Foods)
				{
					o.AddFood(food.Id, food.Name);
				}

				viewModels.Add(o);
			}

			return View(viewModels);
		}

		public ActionResult CurrentOrder()
		{
			return View(CurrentSession.CurrentOrder);
		}
	}
}
