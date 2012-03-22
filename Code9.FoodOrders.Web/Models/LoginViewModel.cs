using System.ComponentModel.DataAnnotations;
using System.Web.Configuration;
using System.Web.Security;
using Code9.FoodOrders.Data;

namespace Code9.FoodOrders.Web.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Username is required")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }
	}
}