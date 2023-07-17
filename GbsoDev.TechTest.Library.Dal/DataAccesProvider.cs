using GbsoDev.TechTest.Library.Dal.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDev.TechTest.Library.Dal
{
	public static class DataAccesProvider
	{
		public static IServiceCollection AddDbContext(IServiceCollection services, string connectionString)
		{
			return services.AddDbContext<RootContext>(options => options.UseSqlServer(connectionString))
				.AddScoped<IRootContext, RootContext>()
				.AddScoped(serviceProvider => new Lazy<IRootContext>(() => serviceProvider.GetRequiredService<IRootContext>()));
		}

		public static IServiceCollection AddDataAcces(this IServiceCollection services)
		{
			return services
				.AddScoped<IUserDal, UserDal>()
				.AddScoped(serviceProvider => new Lazy<IUserDal>(() => serviceProvider.GetRequiredService<IUserDal>()))
				.AddScoped<ILibroDal, LibroDal>()
				.AddScoped(serviceProvider => new Lazy<ILibroDal>(() => serviceProvider.GetRequiredService<ILibroDal>()))
				.AddScoped<IAutorDal, AutorDal>()
				.AddScoped(seriveProvider => new Lazy<IAutorDal>(() => seriveProvider.GetRequiredService<IAutorDal>()))
				.AddScoped<IEditorialDal, EditorialDal>()
				.AddScoped(seriveProvider => new Lazy<IEditorialDal>(() => seriveProvider.GetRequiredService<IEditorialDal>()));
		}
	}
}
