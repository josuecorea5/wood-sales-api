using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WoodSalesApi.Seed
{
	public static class SeedExtension
	{
		public static void Seed(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<IdentityRole>().HasData(
					new IdentityRole(Roles.Admin.ToString()),
					new IdentityRole(Roles.User.ToString())
				);
		}
	}
}
