using GbsoDev.TechTest.Library.Dal.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDev.TechTest.Library.Dal
{
	public static class DataAccesProvider
	{
		public static IServiceCollection AddDbContext(IServiceCollection services, string connectionString) => services.AddDbContext<RootContext>(options => options.UseSqlServer(connectionString));
		
		public static IServiceCollection AddDataAcces(this IServiceCollection services)
		{
			return services
				.AddScoped<ILibroDal, LibroDal>()
				.AddScoped(serviceProvider => new Lazy<ILibroDal>(() => serviceProvider.GetRequiredService<ILibroDal>()))
				.AddScoped<IAutorDal, AutorDal>()
				.AddScoped(seriveProvider => new Lazy<IAutorDal>(() => seriveProvider.GetRequiredService<IAutorDal>()))
				.AddScoped<IEditorialDal, EditorialDal>()
				.AddScoped(seriveProvider => new Lazy<IEditorialDal>(() => seriveProvider.GetRequiredService<IEditorialDal>()));
		}
	}
}
