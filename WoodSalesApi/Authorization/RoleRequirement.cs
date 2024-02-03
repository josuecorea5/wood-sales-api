using Microsoft.AspNetCore.Authorization;

namespace WoodSalesApi.Authorization
{
	public class RoleRequirement : IAuthorizationRequirement
	{
        public string RequiredRole { get; set; }
        public RoleRequirement(string requiredRole)
		{
			RequiredRole = requiredRole;
		}
	}
}
