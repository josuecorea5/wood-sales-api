using System.Text.Json;

namespace WoodSalesApi.Middlewares
{
	public class CustomAuthorizationMiddleware
	{
		private readonly RequestDelegate _next;

		public CustomAuthorizationMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			await _next(context);

			if(context.Response.StatusCode == 403)
			{
				context.Response.StatusCode = 401;
				context.Response.ContentType = "application/json";
				var error = JsonSerializer.Serialize(new { message = "Unauthorized" });
				await context.Response.WriteAsync(error);
			}
		}
	}
}
