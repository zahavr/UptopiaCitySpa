using API.Presentation;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
	public static class ApplicationPresentationExtensions
	{
		public static IServiceCollection AddPresentation(this IServiceCollection services)
		{
			services.AddScoped<IBuildingPresentation, BuildingPresntation>();
			services.AddScoped<IUserPresentation, UserPresentation>();

			return services;
		}
	}
}
