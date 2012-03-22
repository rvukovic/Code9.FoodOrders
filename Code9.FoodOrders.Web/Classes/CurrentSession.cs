using System.Web;
using Code9.FoodOrders.Web.Models;

namespace Code9.FoodOrders.Web.Classes
{
	public class CurrentSession
	{
		public static OrderViewModel CurrentOrder
		{
			get
			{
				OrderViewModel retVal = HttpContext.Current.Session["CurrentOrder"] as OrderViewModel;
				if (retVal == null)
				{
					retVal = new OrderViewModel();
					HttpContext.Current.Session["CurrentOrder"] = retVal;
				}

				return retVal;
			}

			set { HttpContext.Current.Session["CurrentOrder"] = value; }
		}

		public static void ClearOrder()
		{
			HttpContext.Current.Session["CurrentOrder"] = new OrderViewModel();
		}
	}
}