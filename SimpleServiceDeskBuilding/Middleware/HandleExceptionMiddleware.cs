using SimpleServiceDeskBuilding.Exceptions;
using SimpleServiceDeskBuilding.Message;

namespace SimpleServiceDeskBuilding.Middleware
{
	public class HandleExceptionMiddleware : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (NotFoundException e)
			{
				var message = new DefaultMessage
				{
					isSuccess = false,
					message = e.Message,
					statusCode = (int)StatusCodes.Status404NotFound
				};

				context.Response.StatusCode = (int)StatusCodes.Status404NotFound;
				await context.Response.WriteAsJsonAsync(message);
			}
			catch(UnauthorizedException e)
			{
				var message = new DefaultMessage
				{
					isSuccess = false,
					message = e.Message,
					statusCode = (int)StatusCodes.Status401Unauthorized
				};

				context.Response.StatusCode = (int)StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsJsonAsync(message);
			}
			catch (BadRequestException e)
			{
				var message = new DefaultMessage
				{
					isSuccess = false,
					message = e.Message,
					statusCode = (int)StatusCodes.Status400BadRequest
				};

				context.Response.StatusCode = (int)StatusCodes.Status400BadRequest;
				await context.Response.WriteAsJsonAsync(message);
			}
			catch (Exception e)
			{
				var message = new DefaultMessage
				{
					isSuccess = false,
					message = e.Message,
					statusCode = (int)StatusCodes.Status500InternalServerError
				};

				context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
				await context.Response.WriteAsJsonAsync(message);
			}

		}
	}
}
