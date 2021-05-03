using API.Extensions;
using API.Helpers.MapperProfiles;
using API.Middleware;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddDbContext<AppIdentityDbContext>(db =>
            {
                db.UseSqlServer(_configuration.GetConnectionString("IdentityConnection"));
            });
            services.AddControllers();
            services.AddServices();
            services.AddIdentityServices(_configuration);
            services.AddAutoMapper(typeof(UserProfile));
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
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../client";

                spa.UseProxyToSpaDevelopmentServer("https://localhost:4200");
            });
        }
    }
}
