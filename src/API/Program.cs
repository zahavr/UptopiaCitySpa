using API.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.Threading.Tasks;

namespace API
{
	public class Program
    {
        public async static Task Main(string[] args)
        {
			Logger logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

			try
			{
                logger.Debug("Start application...");
                IHost host = CreateHostBuilder(args).Build();

                await host.ApplyMigrations();

                host.Run();
            }
            catch(Exception ex)
            { 
                logger.Error(ex, ex.Message);
            }
			finally
			{
                LogManager.Shutdown();
			}
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging => {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog();
    }
}
