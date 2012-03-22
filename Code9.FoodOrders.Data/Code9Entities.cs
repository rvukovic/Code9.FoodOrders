using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Code9.FoodOrders.Data
{
	public class Code9Entities : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Food> Foods { get; set; }
		public DbSet<Order> Orders { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{

			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}
