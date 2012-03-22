using System.Web.Mvc;

namespace Code9.FoodOrders.Web.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return RedirectToAction("Index", "Food");
		}
	}
}
