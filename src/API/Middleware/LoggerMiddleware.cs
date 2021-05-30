using Infrastructure.Errors;
using Microsoft.AspNetCore.Http;
using NLog;
using NLog.Web;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Middleware
{
	public class LoggerMiddleware
	{
		private readonly RequestDelegate _next;

		public LoggerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			Logger _logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

			Log log = new Log
			{
				Path = context.Request.Path,
				Method = context.Request.Method,
				QueryString = context.Request.QueryString.ToString()
			};

			if (context.Request.Method == HttpMethods.Post)
			{
				context.Request.EnableBuffering();
				string body = await new StreamReader(context.Request.Body)
						.ReadToEndAsync();

				context.Request.Body.Position = 0;
				log.Payload = body;
			}

			log.RequestedOn = DateTime.Now;

			await _next.Invoke(context);

			using (Stream originalRequest = context.Response.Body)
			{
				try
				{
					using (var memStream = new MemoryStream())
					{
						context.Response.Body = memStream;
						memStream.Position = 0;
						var response = await new StreamReader(memStream)
																.ReadToEndAsync();
						log.Response = response;
						log.ResponseCode = context.Response.StatusCode.ToString();
						log.IsSuccessStatusCode = (
							  context.Response.StatusCode == 200 ||
							  context.Response.StatusCode == 201);
						log.RespondedOn = DateTime.Now;

						JsonSerializerOptions options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

						string json = JsonSerializer.Serialize(log, options);

						_logger.Debug(json);

						memStream.Position = 0;

						await memStream.CopyToAsync(originalRequest);
					}
				}
				catch (Exception ex)
				{
					_logger.Error(ex, ex.Message);
				}
				finally
				{
					context.Response.Body = originalRequest;
				}
			}
		}
	}
}
