using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GbsoDev.TechTest.Library.UnitTest
{
	public class CustomServiceCollection : ServiceCollection, IServiceCollection
	{

		/// <summary>
		/// Fabrica de loggers
		/// </summary>
		private static ILoggerFactory loggerFactory;

		public CustomServiceCollection()
		{
			loggerFactory = LoggerFactory.Create(builder =>
			{
				builder.AddProvider(NullLoggerProvider.Instance);
			});
		}

		/// <summary>
		/// Inyecta un nuevo servicio
		/// </summary>
		/// <typeparam name="T">Tipo de servicio</typeparam>
		/// <param name="service">Instancia de servico</param>
		/// <returns></returns>
		public CustomServiceCollection CustomAddScoped<T>(T service) where T : class
		{
			if (service == null) throw new ArgumentNullException($"Argumento requerido: {nameof(service)}");
			this.AddScoped(_ => service)
				.AddScoped(serviceProvider => new Lazy<T>(() => serviceProvider.GetRequiredService<T>()));
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T">tipo de servicio</typeparam>
		/// <param name="service">instandcia de servicio</param>
		/// <returns>Instancia de CustomServiceProvider</returns>
		/// <exception cref="ArgumentNullException">el servicio es null</exception>
		public CustomServiceCollection CustomAddSingleton<T>(T service) where T : class
		{
			if (service == null) throw new ArgumentNullException($"Argumento requerido: {nameof(service)}");
			this.AddSingleton(_ => service);
			return this;
		}

		/// <summary>
		/// Agrega una colección de servicios
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		internal CustomServiceCollection CustomAddRange(IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException($"Argumento requerido: {nameof(services)}");
			var newServices = services.Except(this).ToArray();
			this.Add(newServices);
			return this;
		}

		/// <summary>
		/// Aggrega los loggers requeridos por lo capas base
		/// </summary>
		/// <param name="serviceProvider"></param>
		/// <returns></returns>
		public CustomServiceCollection AddLoggers<T>()
		{
			var logger = loggerFactory.CreateLogger<T>();
			return CustomAddSingleton(logger);
		}
	}
}
