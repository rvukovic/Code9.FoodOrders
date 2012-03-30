using System.Web.Mvc;
using Code9.FoodOrders.Data;
using Code9.FoodOrders.Web.Classes;
using Code9.FoodOrders.Web.Models;


namespace Code9.FoodOrders.Web.Controllers
{
	[Authorize]
	public class FoodController : Controller
	{
		private ICode9Service _service;
		public FoodController(ICode9Service service)
		{
			_service = service;
		}
		
		public ActionResult Index()
		{
			return View(_service.GetAllFoods());
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Food f)
		{
			if (ModelState.IsValid)
			{
				_service.AddFood(f);
				return RedirectToAction("Create");
			}

			return View(f);
		}

		public ActionResult Edit(int id)
		{
			return View(_service.GetFood(id));
		}

		[HttpPost]
		public ActionResult Edit(Food f)
		{
			_service.EditFood(f);
			return View("Details", f);
		}

		public ActionResult Details(int id)
		{
			return View(_service.GetFood(id));
		}

		public ActionResult Delete(int id)
		{
			_service.RemoveFood(id);
			return RedirectToAction("Index");
		}

		public ActionResult AddFoodToOrder(int foodId)
		{
			var food = _service.GetFood(foodId);
			if (food != null)
			{
				CurrentSession.CurrentOrder.AddFood(foodId, food.Name);
			}

			return RedirectToAction("Index");
		}
	}
}
