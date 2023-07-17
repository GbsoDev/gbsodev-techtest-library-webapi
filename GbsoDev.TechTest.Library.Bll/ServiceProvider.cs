using GbsoDev.TechTest.Library.Bll.Contracts;
using GbsoDev.TechTest.Library.Dal;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDev.TechTest.Library.Bll
{
	public static class ServiceProvider
	{
		public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString) => DataAccesProvider.AddDbContext(services, connectionString);

		public static IServiceCollection AddDataAcces(this IServiceCollection services) => DataAccesProvider.AddDataAcces(services);
		
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			return services
				.AddScoped<IAuthService, AuthService>()
				.AddScoped(serviceProvider => new Lazy<IAuthService>(() => serviceProvider.GetRequiredService<IAuthService>()))
				.AddScoped<IAutorService, AutorService>()
				.AddScoped(serviceProvider => new Lazy<IAutorService>(() => serviceProvider.GetRequiredService<IAutorService>()))
				.AddScoped<ILibroService, LibroService>()
				.AddScoped(serviceProvider => new Lazy<ILibroService>(() => serviceProvider.GetRequiredService<ILibroService>()))
				.AddScoped<IEditorialService, EditorialService>()
				.AddScoped(serviceProvider => new Lazy<IEditorialService>(() => serviceProvider.GetRequiredService<IEditorialService>()));
		}
	}
}
