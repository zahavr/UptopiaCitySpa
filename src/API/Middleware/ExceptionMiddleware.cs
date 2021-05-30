using Infrastructure.Erros;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;
		private readonly IHostEnvironment _env;

		public ExceptionMiddleware(
			RequestDelegate next,
			ILogger<ExceptionMiddleware> logger,
			IHostEnvironment env
			)
		{
			_next = next;
			_logger = logger;
			_env = env;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogTrace("bbb");
				_logger.LogDebug("b1b1");
				_logger.LogError(ex, ex.Message);
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				var response = _env.IsDevelopment()
					? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString(), context.Request.Path)
					: new ApiException((int)HttpStatusCode.InternalServerError);

				JsonSerializerOptions options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

				string json = JsonSerializer.Serialize(response, options);

				await SendToTelegram(json);
				
				await context.Response.WriteAsync(json);
			}
		}

		private async Task SendToTelegram(string error)
		{
			await new HttpClient().PostAsync("https://23252931804f.ngrok.io/api/logger/logs",
				new StringContent(error, Encoding.UTF8, "application/json"));
		}
	}
}
