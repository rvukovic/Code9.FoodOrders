using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Code9.FoodOrders.Web.Classes
{
	public class ArrayNotEmptyAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			if (value != null)
			{
				var a = value as IList;
				if (a != null && a.Count > 0)
				{
					return true;
				}
			}

			return false;
		}
	}
}