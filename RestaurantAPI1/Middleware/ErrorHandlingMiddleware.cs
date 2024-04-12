
using Microsoft.AspNetCore.Mvc.Filters;
using RestaurantAPI1.Exceptions;

namespace RestaurantAPI1.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next.Invoke(context);
			}
			catch(NotFoundException e)
			{
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(e.Message);
            }
            catch (Exception e)
			{
				context.Response.StatusCode = 500;
				await context.Response.WriteAsync("Something went wrong");
			}
        }
    }
}
