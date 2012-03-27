using System.Web.Mvc;
using Microsoft.Web.Mvc;
 
[assembly: WebActivator.PreApplicationStartMethod(typeof(Code9.FoodOrders.Web.App_Start.MobileViewEngines), "Start")]
namespace Code9.FoodOrders.Web.App_Start {
    public static class MobileViewEngines{
        public static void Start() 
        {
			ViewEngines.Engines.Insert(0, new MobileCapableRazorViewEngine("Mobile", c => true));
            //ViewEngines.Engines.Insert(0, new MobileCapableWebFormViewEngine());
        }
    }
}