using System;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Code9.FoodOrders.Data;
using Code9.FoodOrders.Web.Classes;
using Code9.FoodOrders.Web.Models;

namespace Code9.FoodOrders.Web.Controllers
{
	public class AccountController : Controller
	{
		private ICode9Service _service;
		public AccountController(ICode9Service service)
		{
			_service = service;
		}

		public ActionResult LogOn()
		{
			return View(new LoginViewModel());
		}

		[HttpPost]
		public ActionResult LogOn(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var hash = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password,
				                                                                  FormsAuthPasswordFormat.SHA1.ToString());
				if (_service.UserFound(model.UserName, hash))
				{
					FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
					return RedirectToAction("Index", "Home");
				}
				
				ModelState.AddModelError(String.Empty, "Username or password is incorrect");
			}

			return View(model);
		}

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register(LoginViewModel newRegistration)
		{
			if (ModelState.IsValid)
			{
				if (!_service.UsernameExists(newRegistration.UserName))
				{
					var hash = FormsAuthentication.HashPasswordForStoringInConfigFile(newRegistration.Password,
					                                                                  FormsAuthPasswordFormat.SHA1.ToString());
					_service.AddUser(newRegistration.UserName, hash);
					FormsAuthentication.SetAuthCookie(newRegistration.UserName, false /* createPersistentCookie */);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError(String.Empty, "This username is already taken.");
				}
			}

			return View(newRegistration);
		}

		public ActionResult LogOff()
		{
			Session.Clear();
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Home");
		}
	}
}
