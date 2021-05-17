using API.Extensions;
using API.Helpers.MapperProfiles;
using API.Hubs;
using API.Hubs.Helpers;
using API.Middleware;
using Azure.Storage.Blobs;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
	public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerDocumentation();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../client/dist";
            });

            services.AddSingleton(bs => 
                new BlobServiceClient(_configuration.GetConnectionString("AzureBlobStorage")));

            services.AddDbContext<AppDbContext>(db =>
            {
                db.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<AppIdentityDbContext>(db =>
            {
                db.UseSqlServer(_configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddPresentation();
            services.AddServices();
            services.AddIdentityServices(_configuration);
            services.AddSignalR();
            services.AddSignalRCore();
            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
            services.AddAutoMapper(typeof(UserProfile), typeof(BuildingProfile), typeof(BusinessProfile));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseSwaggerDocumentation();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("/message");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../client";

                spa.UseProxyToSpaDevelopmentServer("https://localhost:4200");
            });
        }
    }
}
