using Skillfactory.Module32.MVCStartWebApp;

namespace Skillfactory.Module32.ASPCoreMVC.Middleware;
public class LoggingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RequestLog.txt");

	public LoggingMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
	{
		// There was a problem with scope on repository, so i did a rewrite to get repo from DI for every call.
		using (var scope = serviceProvider.CreateScope())
		{
			var requestsRepository = scope.ServiceProvider.GetRequiredService<IRequestsRepository>();
			Request newRequest = CreateRequest(context);
			string logMessage = $"[{newRequest.Date}]: New request to {newRequest.Url}{Environment.NewLine}";
			Console.WriteLine(logMessage);

			// Do we really need await logger to finish?
			requestsRepository.AddRequest(newRequest);
			File.AppendAllTextAsync(LogFilePath, logMessage);
		}
		await _next.Invoke(context);
	}

	private Request CreateRequest(HttpContext context)
	{
		Request newRequest = new();
		newRequest.Id = Guid.NewGuid();
		newRequest.Date = DateTime.Now;
		newRequest.Url = $"{context.Request.Scheme}://{context.Request.Host.Value + context.Request.Path}";
		return newRequest;
	}
}
