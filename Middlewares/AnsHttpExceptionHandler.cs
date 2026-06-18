using Microsoft.AspNetCore.Http;

namespace Ans.Net10.Web.Middlewares
{

	/*
	 *	LibStartup.Use_AnsNet10Web()
	 *		app.UseMiddleware<AnsHttpExceptionHandler>();
	 */



	public class AnsHttpExceptionHandler(
		RequestDelegate pipeline)
	{
		private readonly RequestDelegate request = pipeline;

		public Task Invoke(
			HttpContext context)
		{
			return InvokeAsync(context);
		}

		async Task InvokeAsync(
			HttpContext context)
		{
			try
			{
				await request(context);
			}
			catch (AnsHttpException exception)
			{
				context.Response.StatusCode = (int)exception.StatusCode;
				context.Response.Headers.Clear();
			}
		}
	}

}
