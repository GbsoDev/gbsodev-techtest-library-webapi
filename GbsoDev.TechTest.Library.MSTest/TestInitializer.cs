using GbsoDev.TechTest.Library.Bll;
using GbsoDev.TechTest.Library.Wal;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDev.TechTest.Library.MSTest
{
	[TestClass]
	public static class TestInitializer
	{
		

		/// <summary>
		/// Proveedor de servicios por inyección
		/// </summary>
		public readonly static CustomServiceCollection ServiceProvider = new CustomServiceCollection();

		/// <summary>
		/// Inicializa las inyecciónes requeridas por los servicios
		/// </summary>
		[AssemblyInitialize]
		public static void AssemblyInitializer(TestContext context)
		{
			var services = new ServiceCollection()
				.AddBllValidationRulesLayer()
				.AddMapperConfiguration();
			ServiceProvider
				.CustomAddRange(services)
				.AddLoggers<BaseController>()
				.AddLoggers<BaseService>();
		}

		
	}
}
