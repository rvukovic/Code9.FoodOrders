using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using Code9.FoodOrders.Data;

namespace Code9.FoodOrders.Web
{
	public static class Bootstrapper
	{
		public static void Initialise()
		{
			var container = BuildUnityContainer();

			DependencyResolver.SetResolver(new UnityDependencyResolver(container));
		}

		private static IUnityContainer BuildUnityContainer()
		{
			var container = new UnityContainer();
			
			container.RegisterType<ICode9Service, Code9Service>();
			container.RegisterType<Code9Entities>(new HierarchicalLifetimeManager());
			container.RegisterControllers();

			return container;
		}
	}
}